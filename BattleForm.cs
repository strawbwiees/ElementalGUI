using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class BattleForm : Form
    {
        // players, hp, turns
        private string player1Character;
        private string player2Character;

        private int player1HP = 100;
        private int player2HP = 100;
//oioioi
        private bool player1Turn = true;

        private bool battleOver = false;

        private bool player1Defending = false;
        private bool player2Defending = false;

        private int player1Specials = 3;
        private int player2Specials = 3;


        // render clock for sprites + attack anim
        private const int RenderIntervalMs = 33;
        private System.Windows.Forms.Timer renderTimer;

        private const int P1 = 0;
        private const int P2 = 1;

        private readonly Dictionary<int, Fighter> fighters = new();

        private Fighter Attacker => fighters[player1Turn ? P1 : P2];
        private Fighter Defender => fighters[player1Turn ? P2 : P1];

        private sealed class Fighter
        {
            public BufferedPictureBox Box = null!;
            public Point Home;
            public int ShakeOffset;
            public int BobOffset;
        }


        // attack timeline (tick counts) - higher = slower move
        private const int BasicFlyTicks = 32;
        private const int BasicHitTick = 33;
        private const int BasicRecoilEndTick = 41;
        private const int SpecialWindupEndTick = 36;
        private const int SpecialFlyStartTick = 37;
        private const int SpecialFlyEndTick = 68;
        private const int SpecialHitTick = 69;
        private const int SpecialRecoilEndTick = 77;

        private const int BasicDamage = 20;
        private const int SpecialDamage = 35;

        private bool isAnimating = false;
        private bool isSpecial = false;
        private int attackTick = 0;
        private int idleTick = 0;

        private Point projectileStartPosition;
        private Point projectileTargetPosition;


        public BattleForm(string player1, string player2)
        {
            InitializeComponent();


            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Elemental Battle";

            player1Character = player1;
            player2Character = player2;

            // P1 left, P2 right
            fighters[P1] = new Fighter { Box = pictureBox1 };
            fighters[P2] = new Fighter { Box = pictureBox2 };

            foreach (var fighter in fighters.Values)
            {
                fighter.Home = fighter.Box.Location;
            }

            renderTimer = new System.Windows.Forms.Timer();
            renderTimer.Interval = RenderIntervalMs;
            renderTimer.Tick += RenderTick;

            this.FormClosing += (s, e) =>
            {
                renderTimer.Stop();

                if (e.CloseReason == CloseReason.UserClosing && !battleOver && !GameFlow.Navigating)
                {
                    GameFlow.NavigateTo(new ChooseCharacter());
                }
            };

            SetupBattle();
        }

        private void SetupBattle()
        {
            label1.Text = player1Character.ToUpper();
            label2.Text = player2Character.ToUpper();

            label3.Text = player1Character.ToUpper() + "'S TURN";

            player1HP = 100;
            player2HP = 100;

            // reset hp bars + labels
            CartoonUI.SetHpBar(hpBar1, 100, 100);
            CartoonUI.SetHpBar(hpBar2, 100, 100);

            label4.Text = "100/100";
            label5.Text = "100/100";

            label6.Text = "Choose your action!";

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            SpriteAnimator.SetSprite(pictureBox1, GetCharacterImage(player1Character));
            SpriteAnimator.SetSprite(pictureBox2, GetCharacterImage(player2Character));

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            UpdateSpecialButton();

            renderTimer.Start();
        }



        private Image? GetCharacterImage(string character)
        {
            if (character == "Lumen")
            {
                return Properties.Resources.fire;
            }

            if (character == "Ripple")
            {
                return Properties.Resources.water;
            }

            if (character == "Grunch")
            {
                return Properties.Resources.earth;
            }

            if (character == "Gale")
            {
                return Properties.Resources.wind;
            }

            return null;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            BeginAttack(isSpecial: false);
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            if (GetSpecialUses() <= 0)
            {
                CartoonUI.PlayBlock();
                label6.Text = "No specials left!";
                return;
            }

            UseSpecial();
            BeginAttack(isSpecial: true);
        }

        private int GetSpecialUses()
        {
            return player1Turn ? player1Specials : player2Specials;
        }

        private void UseSpecial()
        {
            if (player1Turn) player1Specials--;
            else player2Specials--;
        }

        private void UpdateSpecialButton()
        {
            button3.Text = "SPECIAL (" + GetSpecialUses() + ")";
        }



        // kicks off the attack anim
        private void BeginAttack(bool isSpecial)
        {
            isAnimating = true;
            this.isSpecial = isSpecial;
            attackTick = 0;

            DisableButtons();

            string attacker =
                player1Turn
                ? player1Character
                : player2Character;

            Image? projectile;

            if (isSpecial)
            {
                label6.Text = attacker + " used SPECIAL ATTACK!";

                pictureBox3.Image = GetSpecialAttackImage(attacker);
                pictureBox3.Visible = true;

                projectile = GetSpecialProjectileImage(attacker);
                pictureBox4.Visible = false;
            }
            else
            {
                label6.Text = attacker + " used BASIC ATTACK!";

                projectile = GetBasicAttackImage(attacker);
                pictureBox4.Visible = true;
            }

            SpriteAnimator.SetSprite(pictureBox4, projectile);

            // where the shot starts and where it lands
            ComputeProjectilePath();
            pictureBox4.Location = projectileStartPosition;
        }



        private void ComputeProjectilePath()
        {
            if (player1Turn)
            {
                projectileStartPosition = new Point(
                    pictureBox1.Right - 30,
                    pictureBox1.Top + pictureBox1.Height / 2 - 50
                );

                projectileTargetPosition = new Point(
                    pictureBox2.Left,
                    pictureBox2.Top + pictureBox2.Height / 2 - 50
                );
            }
            else
            {
                projectileStartPosition = new Point(
                    pictureBox2.Left - 100,
                    pictureBox2.Top + pictureBox2.Height / 2 - 50
                );

                projectileTargetPosition = new Point(
                    pictureBox1.Right - 30,
                    pictureBox1.Top + pictureBox1.Height / 2 - 50
                );
            }
        }


        private Image? GetBasicAttackImage(string character)
        {
            if (character == "Lumen")
            {
                return Properties.Resources.FireAttack;
            }

            if (character == "Ripple")
            {
                return Properties.Resources.WaterAttack;
            }

            if (character == "Grunch")
            {
                return Properties.Resources.EarthAttack;
            }

            if (character == "Gale")
            {
                return Properties.Resources.WindAttack;
            }

            return null;
        }


        private Image? GetSpecialAttackImage(string character)
        {
            if (character == "Lumen")
            {
                return Properties.Resources.FireBG;
            }

            if (character == "Ripple")
            {
                return Properties.Resources.WaterBG;
            }

            if (character == "Grunch")
            {
                return Properties.Resources.EarthBG;
            }

            if (character == "Gale")
            {
                return Properties.Resources.WindBG;
            }

            return null;
        }


        private Image? GetSpecialProjectileImage(string character)
        {
            if (character == "Lumen")
            {
                return Properties.Resources.FireAttack;
            }

            if (character == "Ripple")
            {
                return Properties.Resources.WaterAttack;
            }

            if (character == "Grunch")
            {
                return Properties.Resources.EarthAttack;
            }

            if (character == "Gale")
            {
                return Properties.Resources.WindAttack;
            }

            return null;
        }



        // single clock: sprites, idle bob, attack anim
        private void RenderTick(object? sender, EventArgs e)
        {
            SpriteAnimator.Tick();

            if (isAnimating)
            {
                RunAttackTimeline();
            }
            else
            {
                idleTick++;

                // half-second bob cycle
                fighters[P1].BobOffset = (idleTick % 16 < 8) ? -4 : 2;
                fighters[P2].BobOffset = (idleTick % 16 < 8) ? 4 : -2;
            }

            ApplyFighterVisuals();
        }

        // home pos + shake + bob
        private void ApplyFighterVisuals()
        {
            foreach (var fighter in fighters.Values)
            {
                fighter.Box.Location = new Point(
                    fighter.Home.X + fighter.ShakeOffset,
                    fighter.Home.Y + fighter.BobOffset
                );
            }
        }



        // fly -> hit -> recoil -> done
        private void RunAttackTimeline()
        {
            attackTick++;

            int flyTicks = isSpecial ? SpecialFlyEndTick - SpecialFlyStartTick
                                     : BasicFlyTicks;
            int hitTick = isSpecial ? SpecialHitTick : BasicHitTick;


            // special windup: effect covers the screen first
            if (isSpecial && attackTick <= SpecialWindupEndTick)
            {
                return;
            }


            if (attackTick <= hitTick - 1)
            {
                int flown = isSpecial
                    ? attackTick - SpecialFlyStartTick
                    : attackTick;

                if (isSpecial && attackTick == SpecialFlyStartTick)
                {
                    pictureBox3.Visible = false;
                    pictureBox4.Visible = true;
                }

                float progress = Math.Min(1f, flown / (float)flyTicks);

                pictureBox4.Location = new Point(
                    projectileStartPosition.X +
                        (int)((projectileTargetPosition.X - projectileStartPosition.X) * progress),
                    projectileStartPosition.Y +
                        (int)((projectileTargetPosition.Y - projectileStartPosition.Y) * progress)
                );

                return;
            }


            // hit lands: damage + knockback
            if (attackTick == hitTick)
            {
                pictureBox4.Visible = false;

                bool hitPlayer2 = player1Turn;
                int damage = isSpecial ? SpecialDamage : BasicDamage;
                int knockback = isSpecial ? 20 : 15;

                ApplyDamage(hitPlayer2, damage);

                Defender.ShakeOffset = Defender == fighters[P2] ? knockback : -knockback;

                return;
            }


            int recoilEnd = isSpecial ? SpecialRecoilEndTick : BasicRecoilEndTick;
            int recoilStep = isSpecial ? 3 : 2;

            if (attackTick <= recoilEnd)
            {
                // settle back gently
                Defender.ShakeOffset += Defender == fighters[P2] ? -recoilStep : recoilStep;

                return;
            }


            EndAttack();
        }



        private async void ScreenShake(int intensity)
        {
            Point original = this.Location;
            Random rand = new Random();

            for (int i = 0; i < 8; i++)
            {
                int dx = rand.Next(-intensity, intensity + 1);
                int dy = rand.Next(-intensity, intensity + 1);
                this.Location = new Point(original.X + dx, original.Y + dy);
                await System.Threading.Tasks.Task.Delay(25);
            }

            this.Location = original;
        }



        // guard halves the hit, else hp drops
        private void ApplyDamage(bool toPlayer2, int damage)
        {
            bool defending = toPlayer2 ? player2Defending : player1Defending;
            string targetName = toPlayer2 ? player2Character : player1Character;
            var targetPb = (toPlayer2 ? fighters[P2] : fighters[P1]).Box;
            int impactX = targetPb.Left + targetPb.Width / 2;
            int impactY = targetPb.Top + targetPb.Height / 3;

            if (defending)
            {
                CartoonUI.PlayBlock();

                // guard breaks after one hit
                if (toPlayer2) player2Defending = false;
                else player1Defending = false;

                var _ = CartoonUI.ShowBurstAsync(this, new Point(impactX, impactY), "BLOCKED!", Color.SkyBlue);

                label6.Text = targetName + " blocked the attack!";
            }
            else
            {
                CartoonUI.PlayPunch();

                if (toPlayer2)
                {
                    player2HP = Math.Max(0, player2HP - damage);
                    CartoonUI.SetHpBar(hpBar2, player2HP, 100);
                    label5.Text = player2HP + "/100";
                }
                else
                {
                    player1HP = Math.Max(0, player1HP - damage);
                    CartoonUI.SetHpBar(hpBar1, player1HP, 100);
                    label4.Text = player1HP + "/100";
                }

                var _ = CartoonUI.ShowBurstAsync(this, new Point(impactX, impactY), "POW!", CartoonUI.BadColor);

                label6.Text = targetName + " took " + damage + " damage!";
            }

            ScreenShake(defending ? 3 : 6);
        }



        // cleanup, then next turn (or game over)
        private void EndAttack()
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            foreach (var fighter in fighters.Values)
            {
                fighter.ShakeOffset = 0;
                fighter.BobOffset = 0;
            }

            ApplyFighterVisuals();

            if (player1HP <= 0)
            {
                // someone's down
                EndBattle(player2Character);
                return;
            }

            if (player2HP <= 0)
            {
                EndBattle(player1Character);
                return;
            }

            player1Turn = !player1Turn;

            UpdateTurn();

            isAnimating = false;

            EnableButtons();
        }



        // guard: halves next hit taken, ends your turn
        private void button2_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            string character =
                player1Turn
                ? player1Character
                : player2Character;

            if (player1Turn) player1Defending = true;
            else player2Defending = true;

            CartoonUI.PlayBlock();

            label6.Text =
                character +
                " raises a guard! (next hit -50%)";

            player1Turn = !player1Turn;

            UpdateTurn();
        }



        private void UpdateTurn()
        {
            if (player1Turn)
            {
                label3.Text =
                    player1Character.ToUpper() +
                    "'S TURN";
            }
            else
            {
                label3.Text =
                    player2Character.ToUpper() +
                    "'S TURN";
            }

            UpdateSpecialButton();

            label6.Text =
                "Choose your action!";
        }



        private void DisableButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }


        private void EnableButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = GetSpecialUses() > 0;
        }



        // show winner, then rematch or quit
        private void EndBattle(string winner)
        {
            renderTimer.Stop();

            battleOver = true;

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            DisableButtons();

            label3.Text =
                "BATTLE OVER";

            label6.Text =
                winner.ToUpper() +
                " WINS!";

            CartoonUI.PlayWin();

            var _ = CartoonUI.ShowBurstAsync(
                this,
                new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 3),
                "K.O.!",
                CartoonUI.GoldColor);

            MessageBox.Show(
                winner + " wins!",
                "Battle Finished",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            var result = MessageBox.Show(
                "Play again?",
                "Rematch",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                GameFlow.NavigateTo(new ChooseCharacter());
            }
            else
            {
                Application.Exit();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
