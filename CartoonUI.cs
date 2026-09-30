using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace ElementalGUI
{
    internal static class CartoonUI
    {
        // fonts + palette used everywhere
        public static readonly Font TitleFont = new Font("Comic Sans MS", 48F, FontStyle.Bold);
        public static readonly Font HeadingFont = new Font("Comic Sans MS", 28F, FontStyle.Bold);
        public static readonly Font BigFont = new Font("Comic Sans MS", 25.8F, FontStyle.Bold);
        public static readonly Font MediumFont = new Font("Comic Sans MS", 16.2F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Comic Sans MS", 13.8F, FontStyle.Bold);

        public static readonly Color InkColor = Color.FromArgb(28, 20, 44);      // Deep purple ink
        public static readonly Color HighlightColor = Color.FromArgb(255, 244, 100); // Sunbeam yellow
        public static readonly Color PanelColor = Color.FromArgb(44, 32, 66);    // Panel purple
        public static readonly Color GoodColor = Color.FromArgb(126, 217, 87);   // Slime green
        public static readonly Color BadColor = Color.FromArgb(255, 99, 92);     // Coral red
        public static readonly Color GoldColor = Color.FromArgb(255, 209, 61);   // Coin gold


        // flat colored button, white text
        public static void StyleButton(Button button, Color face)
        {
            button.BackColor = face;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
        }

        public static void DrawButtonOutline(PaintEventArgs e, Button button)
        {
            using var ink = new Pen(InkColor, 3);
            e.Graphics.DrawRectangle(ink, 1, 1, button.Width - 3, button.Height - 3);
        }


        public static void SetHpBar(Control bar, int current, int max)
        {
            if (bar is HpBar hp)
                hp.SetValues(current, max);
        }

        // rounded hp bar, green -> gold -> red
        public sealed class HpBar : Control
        {
            private float _percent = 1f;

            public HpBar()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            }

            public void SetValues(int current, int max)
            {
                _percent = max <= 0 ? 0f : Math.Clamp(current / (float)max, 0f, 1f);
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                int w = Width - 1, h = Height - 1;

                using var backBrush = new SolidBrush(Color.FromArgb(90, 10, 8, 24));
                using var path = RoundedRect(new RectangleF(0, 0, w, h), h / 2f);
                g.FillPath(backBrush, path);

                if (_percent > 0)
                {
                    Color fill = _percent > 0.5f ? GoodColor
                               : _percent > 0.25f ? GoldColor
                               : BadColor;

                    var fillRect = new RectangleF(2, 2, (w - 4) * _percent, h - 4);

                    using var fillBrush = new LinearGradientBrush(
                        new RectangleF(0, 0, w, h),
                        ControlPaint.Light(fill), fill, LinearGradientMode.Vertical);
                    using var fillPath = RoundedRect(fillRect, (h - 4) / 2f);
                    g.FillPath(fillBrush, fillPath);

                    using var shine = new SolidBrush(Color.FromArgb(70, Color.White));                    using var shinePath = RoundedRect(
                        new RectangleF(fillRect.X + 2, fillRect.Y + 2, Math.Max(0, fillRect.Width - 4), (h - 4) / 2.6f),
                        (h - 4) / 4f);
                    g.FillPath(shine, shinePath);
                }

                using var ink = new Pen(InkColor, 3);
                g.DrawPath(ink, path);
            }

            private static GraphicsPath RoundedRect(RectangleF r, float radius)
            {
                var path = new GraphicsPath();
                if (r.Width <= 0 || r.Height <= 0)
                {
                    path.AddRectangle(new RectangleF(r.X, r.Y, 1, 1));
                    return path;
                }
                float d = radius * 2;
                if (d > r.Width) d = r.Width;
                if (d > r.Height) d = r.Height;
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                return path;
            }
        }


        // pop a starburst word (POW!, BLOCKED!, K.O.!)
        public static async System.Threading.Tasks.Task ShowBurstAsync(Control host, Point center, string word, Color color)
        {
            var burst = new BurstControl(word, color) { Location = new Point(center.X - 140, center.Y - 90) };
            host.Controls.Add(burst);
            burst.BringToFront();
            await burst.AnimateAsync();
            host.Controls.Remove(burst);
            burst.Dispose();
        }

        private sealed class BurstControl : Control
        {
            private readonly string _word;
            private readonly Color _color;
            private int _tick;

            public BurstControl(string word, Color color)
            {
                _word = word;
                _color = color;
                Size = new Size(280, 180);
                SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.Transparent;
            }

            public async System.Threading.Tasks.Task AnimateAsync()
            {
                for (_tick = 0; _tick <= 14; _tick++)
                {
                    Invalidate();
                    await System.Threading.Tasks.Task.Delay(40);
                }
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float t = _tick / 14f;
                float scale = t < 0.3f ? 0.4f + t * 2f : 1f + 0.08f * (float)Math.Sin(t * Math.PI);
                float alpha = t < 0.75f ? 1f : 1f - (t - 0.75f) / 0.25f;

                var center = new PointF(Width / 2f, Height / 2f);

                // starburst shape
                var points = new PointF[24];
                for (int i = 0; i < 24; i++)
                {
                    double angle = i * Math.PI / 12;
                    float radius = (i % 2 == 0 ? 80f : 55f) * scale;
                    points[i] = new PointF(center.X + (float)Math.Cos(angle) * radius,
                                           center.Y + (float)Math.Sin(angle) * radius);
                }

                using var ink = new Pen(InkColor, 4);
                using var fill = new SolidBrush(Color.FromArgb((int)(255 * alpha), _color));
                g.FillPolygon(fill, points);
                g.DrawPolygon(ink, points);

                using var font = new Font("Comic Sans MS", 24F * scale, FontStyle.Bold);
                using var textInk = new SolidBrush(InkColor);
                var size = g.MeasureString(_word, font);
                g.DrawString(_word, font, textInk,
                    center.X - size.Width / 2f, center.Y - size.Height / 2f);
            }
        }


        // little chiptune bleeps, generated on the fly
        public static void PlayPunch() => PlayTone(160, 0.12, 0.9, "square");
        public static void PlayBlock() => PlayTone(520, 0.10, 0.6, "triangle");
        public static void PlayClick() => PlayTone(660, 0.06, 0.5, "square");
        public static void PlayWin()
        {
            int[] notes = { 523, 659, 784, 1047 };
            foreach (var n in notes) PlayTone(n, 0.15, 0.7, "square");
        }

        // writes a wav in memory and plays it
        private static void PlayTone(double freq, double seconds, double volume, string wave)
        {
            try
            {
                int rate = 22050;
                int samples = (int)(rate * seconds);
                var ms = new MemoryStream(samples * 44);
                using (var writer = new BinaryWriter(ms, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write("RIFF".ToCharArray());
                    writer.Write(36 + samples * 2);
                    writer.Write("WAVE".ToCharArray());
                    writer.Write("fmt ".ToCharArray());
                    writer.Write(16); writer.Write((short)1); writer.Write((short)1);
                    writer.Write(rate); writer.Write(rate * 2);
                    writer.Write((short)2); writer.Write((short)16);
                    writer.Write("data".ToCharArray());
                    writer.Write(samples * 2);

                    for (int i = 0; i < samples; i++)
                    {
                        double t = (double)i / rate;
                        double envelope = Math.Min(1.0, (samples - i) / (rate * 0.05)) * Math.Min(1.0, t * 100);
                        double value = wave switch
                        {
                            "square" => Math.Sign(Math.Sin(2 * Math.PI * freq * t)),
                            "sawtooth" => 2.0 * (t * freq - Math.Floor(t * freq + 0.5)),
                            _ => Math.Sin(2 * Math.PI * freq * t)
                        };
                        short sample = (short)(value * envelope * volume * short.MaxValue * 0.4);
                        writer.Write(sample);
                    }
                }
                ms.Position = 0;
                using var player = new SoundPlayer(ms);
                player.PlaySync();
                ms.Dispose();
            }
            catch { /* Sound is a bonus - never crash the game over it. */ }
        }
    }
}
