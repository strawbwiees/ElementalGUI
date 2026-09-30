param([string]$ExePath = "bin\Debug\net10.0-windows\ElementalGUI.exe")

$ErrorActionPreference = 'Stop'
$log = "playtest.log"
function L($m) { $m | Out-File -FilePath $log -Append -Encoding utf8 }

Set-Content -Path $log -Value "=== playtest $(Get-Date -Format o) ==="

Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes
Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

Add-Type @"
using System;
using System.Runtime.InteropServices;
public static class Native {
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] public static extern void mouse_event(uint f, uint dx, uint dy, uint d, UIntPtr e);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr h);
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] public static extern bool GetClientRect(IntPtr h, out RECT r);
    [DllImport("user32.dll")] public static extern bool ClientToScreen(IntPtr h, ref POINT p);
    [DllImport("user32.dll")] public static extern bool GetCursorPos(out int x, out int y);
    public struct RECT { public int Left, Top, Right, Bottom; }
    public struct POINT { public int X, Y; }
    public static void Click(int x, int y) {
        SetCursorPos(x, y);
        System.Threading.Thread.Sleep(50);
        mouse_event(2, 0, 0, 0, UIntPtr.Zero);
        System.Threading.Thread.Sleep(30);
        mouse_event(4, 0, 0, 0, UIntPtr.Zero);
    }
    public static void ClickClient(IntPtr hwnd, int cx, int cy) {
        var p = new POINT { X = cx, Y = cy };
        ClientToScreen(hwnd, ref p);
        Click(p.X, p.Y);
    }
}
"@

$root = [System.Windows.Automation.AutomationElement]::RootElement
$script:shots = 0
$script:checks = 0
$script:failures = New-Object System.Collections.Generic.List[string]

function Get-AllGameProcs { @(Get-Process ElementalGUI -ErrorAction SilentlyContinue) }
function Get-GameProc { Get-AllGameProcs | Select-Object -First 1 }

function Get-AllWindows {
    $p = Get-GameProc
    if (-not $p) { return @() }
    $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $p.Id)
    $wins = $root.FindAll([System.Windows.Automation.TreeScope]::Children, $cond)
    return @($wins)
}

function Get-Win($titlePart) {
    foreach ($w in (Get-AllWindows)) {
        if ($w.Current.Name -like "*$titlePart*") { return $w }
    }
    $null
}

function Log-Windows($tag) {
    $names = (Get-AllWindows | ForEach-Object { "'" + $_.Current.Name + "'" }) -join ", "
    L ($tag + " windows: " + $names)
}

function Find-ButtonRect($win, $namePart) {
    if (-not $win) { return $null }
    $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Button)
    $buttons = $win.FindAll([System.Windows.Automation.TreeScope]::Descendants, $cond)
    foreach ($b in $buttons) {
        if ($b.Current.Name -like "*$namePart*") { return $b.Current.BoundingRectangle }
    }
    $null
}

function Focus-Win($win) {
    [Native]::SetForegroundWindow([IntPtr]$win.Current.NativeWindowHandle) | Out-Null
    Start-Sleep -Milliseconds 200
}

function Click-Client($win, $cx, $cy, $tag) {
    $h = [IntPtr]$win.Current.NativeWindowHandle
    $client = New-Object Native+RECT
    [Native]::GetClientRect($h, [ref]$client) | Out-Null
    $cw = $client.Right - $client.Left
    $ch = $client.Bottom - $client.Top
    $sx = $cw / 1182.0
    $sy = $ch / 753.0
    $x = [int]($cx * $sx)
    $y = [int]($cy * $sy)
    [Native]::ClickClient($h, $x, $y)
    $pt = New-Object Native+POINT
    $pt.X = $x; $pt.Y = $y
    [Native]::ClientToScreen($h, [ref]$pt) | Out-Null
    $cx2 = 0; $cy2 = 0
    $ok = [Native]::GetCursorPos([ref]$cx2, [ref]$cy2)
    L ("click " + $tag + " client(" + $x + "," + $y + ") -> screen(" + $pt.X + "," + $pt.Y + ") cursor now(" + $cx2 + "," + $cy2 + ") client-size(" + $cw + "x" + $ch + ")")
}

function Press($win, $namePart) {
    $r = Find-ButtonRect $win $namePart
    if (-not $r) { throw "button not found: $namePart" }
    Focus-Win $win
    L ("press " + $namePart + " rect(" + [int]$r.X + "," + [int]$r.Y + " " + [int]$r.Width + "x" + [int]$r.Height + ")")
    # click center of the UIA rect, then verify it did something
    for ($try = 1; $try -le 3; $try++) {
        [Native]::Click([int]($r.X + $r.Width / 2), [int]($r.Y + $r.Height / 2))
        Start-Sleep -Milliseconds 500
        $count = (Get-AllWindows).Count
        L ("press try " + $try + ", open windows now: " + $count)
        if ($count -gt 1) { return }
    }
}

function Get-Label($win, $namePart) {
    $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Text)
    $texts = $win.FindAll([System.Windows.Automation.TreeScope]::Descendants, $cond)
    foreach ($t in $texts) { if ($t.Current.Name -like "*$namePart*") { return $t.Current.Name } }
    $null
}

function Invoke-Dialogs {
    foreach ($w in (Get-AllWindows)) {
        if ($w.Current.ClassName -ne "#32770") { continue }
        $msg = ""
        $tcond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Text)
        foreach ($t in $w.FindAll([System.Windows.Automation.TreeScope]::Descendants, $tcond)) { $msg += $t.Current.Name + " " }
        L ("DIALOG: " + $msg.Trim())
        [Native]::SetForegroundWindow([IntPtr]$w.Current.NativeWindowHandle) | Out-Null
        Start-Sleep -Milliseconds 150
        if ($msg -match "Play again") { [System.Windows.Forms.SendKeys]::SendWait("%n") }
        else { [System.Windows.Forms.SendKeys]::SendWait("{ENTER}") }
        Start-Sleep -Milliseconds 500
        L "DIALOG DISMISSED"
    }
}

function Shot($win, $tag) {
    if (-not $win) { return }
    try {
        $r = $win.Current.BoundingRectangle
        if ($r.Width -le 0) { return }
        $bmp = New-Object System.Drawing.Bitmap([int]$r.Width, [int]$r.Height)
        $g = [System.Drawing.Graphics]::FromImage($bmp)
        $g.CopyFromScreen([int]$r.X, [int]$r.Y, 0, 0, $bmp.Size)
        $g.Dispose()
        $script:shots++
        $f = "shot_{0:D2}_{1}.png" -f $script:shots, $tag
        $bmp.Save($f, [System.Drawing.Imaging.ImageFormat]::Png)
        $bmp.Dispose()
        L ("SCREENSHOT " + $f)
    } catch { L ("SHOT FAILED " + $tag + ": " + $_.Exception.Message) }
}

function Check($desc, $cond) {
    $script:checks++
    if ($cond) { L ("PASS  " + $desc) }
    else { L ("FAIL  " + $desc); $script:failures.Add($desc) }
}

function Click-Card($win, $index) {
    # exact client mapping: card centers at (151|446|738|1026, 403) in a 1182x753 client
    $h = [IntPtr]$win.Current.NativeWindowHandle
    $client = New-Object Native+RECT
    [Native]::GetClientRect($h, [ref]$client) | Out-Null
    $cw = $client.Right - $client.Left
    $ch = $client.Bottom - $client.Top
    $sx = $cw / 1182.0
    $sy = $ch / 753.0
    $xs = @(151, 446, 738, 1026)
    $cx = [int]($xs[$index] * $sx)
    $cy = [int](403 * $sy)
    L ("click card " + $index + " client(" + $cx + "," + $cy + ") client-size(" + $cw + "x" + $ch + ")")
    [Native]::ClickClient($h, $cx, $cy)
    Start-Sleep -Milliseconds 250
}

function Wait-For($titlePart, $seconds) {
    $deadline = (Get-Date).AddSeconds($seconds)
    while ((Get-Date) -lt $deadline) {
        $w = Get-Win $titlePart
        if ($w) { return $w }
        Start-Sleep -Milliseconds 200
    }
    $null
}

try {
    L "=== CLEAN SLATE ==="
    Stop-Process -Name ElementalGUI -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 1
    $left = Get-AllGameProcs
    Check "no zombie processes before launch" ($left.Count -eq 0)

    L "=== LAUNCH ==="
    Start-Process $ExePath
    Start-Sleep -Seconds 3
    Log-Windows "at launch"
    Check "only the menu window exists at launch" ((Get-AllWindows).Count -eq 1)

    $menu = Get-Win "ElementalGUI"
    Check "main menu window appears" ($null -ne $menu)
    if (-not $menu) { throw "no window" }
    Shot $menu "menu"

    L "=== PLAY -> character select ==="
    Press $menu "PLAY"
    $sel = Wait-For "Choose Your Character" 5
    Check "character select window appears" ($null -ne $sel)
    if (-not $sel) { throw "no select window" }
    Start-Sleep -Milliseconds 500
    Log-Windows "on select"
    Shot $sel "select"

    L "=== empty pick shows warning, stays on P1 ==="
    Press $sel "SELECT"
    Start-Sleep -Milliseconds 600
    Invoke-Dialogs
    Start-Sleep -Milliseconds 300
    Invoke-Dialogs
    $lbl = Get-Label $sel "PLAYER"
    L ("label after empty select: '" + $lbl + "'")
    Check "empty select kept us on PLAYER 1" ($lbl -eq "PLAYER 1")
    Shot $sel "warn"

    L "=== P1 picks Lumen ==="
    Click-Card $sel 0
    Start-Sleep -Milliseconds 200
    Press $sel "SELECT"
    Start-Sleep -Milliseconds 800
    Invoke-Dialogs
    $lbl = Get-Label $sel "PLAYER"
    L ("label after P1 pick: '" + $lbl + "'")
    Check "advanced to PLAYER 2" ($lbl -eq "PLAYER 2")

    L "=== P2 picks Ripple ==="
    Click-Card $sel 1
    Start-Sleep -Milliseconds 200
    Shot $sel "p2picked"
    Press $sel "SELECT"
    Start-Sleep -Milliseconds 800
    Invoke-Dialogs

    L "=== CONFIRM ==="
    Log-Windows "after both picks"
    $confirm = Wait-For "Confirm Selection" 5
    Check "confirm window appears" ($null -ne $confirm)
    if (-not $confirm) { throw "no confirm window" }
    Shot $confirm "confirm"
    $c1 = Get-Label $confirm "LUMEN"
    $c2 = Get-Label $confirm "RIPPLE"
    L ("confirm shows: p1='" + $c1 + "' p2='" + $c2 + "'")
    Check "confirm lists LUMEN for p1" ($c1 -eq "LUMEN")
    Check "confirm lists RIPPLE for p2" ($c2 -eq "RIPPLE")
    Press $confirm "CONFIRM"
    Start-Sleep -Milliseconds 800
    Invoke-Dialogs

    L "=== VS ==="
    $vs = Wait-For "Get Ready" 5
    Check "vs window appears" ($null -ne $vs)
    if (-not $vs) { throw "no vs window" }
    $v1 = Get-Label $vs "LUMEN"
    $v2 = Get-Label $vs "RIPPLE"
    L ("vs shows: '" + $v1 + "' vs '" + $v2 + "'")
    Check "vs shows both fighters" ($null -ne $v1 -and $null -ne $v2)
    Shot $vs "vs"
    Press $vs "START BATTLE"
    Start-Sleep -Seconds 2
    Invoke-Dialogs

    L "=== BATTLE ==="
    $battle = Wait-For "Elemental Battle" 5
    Check "battle window appears" ($null -ne $battle)
    if (-not $battle) { throw "no battle window" }
    Start-Sleep -Milliseconds 500
    $turn1 = Get-Label $battle "TURN"
    $hp1 = Get-Label $battle "/100"
    L ("first turn label: '" + $turn1 + "'")
    L ("first hp labels: '" + $hp1 + "'")
    Check "turn label says LUMEN'S TURN" ($turn1 -eq "LUMEN'S TURN")
    Check "hp shows 100/100 at start" ($hp1 -eq "100/100")
    Shot $battle "start"

    $actions = @("BASIC", "DEFEND", "SPECIAL", "BASIC", "SPECIAL", "BASIC", "SPECIAL", "BASIC", "BASIC")
    $i = 0
    foreach ($a in $actions) {
        $i++
        $deadline = (Get-Date).AddSeconds(15)
        $ready = $false
        while ((Get-Date) -lt $deadline) {
            Invoke-Dialogs
            if (Find-ButtonRect $battle $a) { $ready = $true; break }
            Start-Sleep -Milliseconds 200
        }
        if (-not $ready) { L ("button never ready: " + $a + " (battle may be over)"); break }

        Press $battle $a
        Start-Sleep -Milliseconds 800
        Invoke-Dialogs
        if ($i -eq 1) { Shot $battle "anim" }
        Start-Sleep -Milliseconds 1300
        Invoke-Dialogs
        $hpAfter = (Get-Label $battle "/100") -join " | "
        $turnNow = Get-Label $battle "TURN"
        L ("action " + $i + " [" + $a + "] turn='" + $turnNow + "' hp=" + $hpAfter)
        if ($i -eq 3) { Shot $battle "special" }
        if ($i -eq 5) { Shot $battle "mid" }
    }

    Shot $battle "end"

    L "=== KO + DIALOGS ==="
    $sawWins = $false
    $winnerText = ""
    $deadline = (Get-Date).AddSeconds(30)
    while ((Get-Date) -lt $deadline) {
        if (-not (Get-GameProc)) { break }
        foreach ($w in (Get-AllWindows)) {
            if ($w.Current.ClassName -ne "#32770") { continue }
            $tcond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Text)
            $msg = ""
            foreach ($t in $w.FindAll([System.Windows.Automation.TreeScope]::Descendants, $tcond)) { $msg += $t.Current.Name + " " }
            if ($msg -match "wins") { $sawWins = $true; $winnerText = $msg.Trim() }
        }
        if ($sawWins) { break }
        Invoke-Dialogs
        Start-Sleep -Milliseconds 400
    }
    L ("KO dialog: '" + $winnerText + "'")
    Check "saw the winner dialog" $sawWins

    Invoke-Dialogs
    Start-Sleep -Milliseconds 800
    Invoke-Dialogs

    $p = Get-GameProc
    if ($p) {
        Log-Windows "final state"
        Check "after declining rematch the app is still alive" ($null -ne $p)
    } else {
        Check "app exited cleanly after declining rematch" $true
    }

    L ("RESULT: " + $script:checks + " checks, " + $script:failures.Count + " failures, " + $script:shots + " shots")
    if ($script:failures.Count -gt 0) {
        foreach ($f in $script:failures) { L ("FAILED: " + $f) }
        L "NOT ALL GREEN"
    } else {
        L "ALL GREEN"
    }
}
catch {
    L ("EXCEPTION: " + $_.Exception.Message)
    Log-Windows "exception state"
    L "NOT ALL GREEN"
}
finally {
    Stop-Process -Name ElementalGUI -Force -ErrorAction SilentlyContinue
    L "cleanup done"
}
