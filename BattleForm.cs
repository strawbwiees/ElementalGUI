
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class BattleForm : Form
    {

        // =========================================================
        // CHARACTER DATA
        // =========================================================

        private string player1Character;
        private string player2Character;

        private int player1HP = 100;
        private int player2HP = 100;

        private bool player1Turn = true;

        // =========================================================
        // ANIMATION
        // =========================================================

        private System.Windows.Forms.Timer animationTimer;

        private int animationStep = 0;

        private bool isAnimating = false;

        private string currentAnimation = "";

        private Point player1StartPosition;
        private Point player2StartPosition;

        // Projectile starting and target positions
        private Point projectileStartPosition;
        private Point projectileTargetPosition;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public BattleForm(string player1, string player2)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            player1Character = player1;
            player2Character = player2;

            // Remember original character positions
            player1StartPosition = pictureBox1.Location;
            player2StartPosition = pictureBox2.Location;

            // Setup animation timer
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 20;
            animationTimer.Tick += AnimationTimer_Tick;

            SetupBattle();
        }


        // =========================================================
        // SETUP BATTLE
        // =========================================================

        private void SetupBattle()
        {
            label1.Text = player1Character.ToUpper();
            label2.Text = player2Character.ToUpper();

            label3.Text = player1Character.ToUpper() + "'S TURN";

            // HP
            player1HP = 100;
            player2HP = 100;

            progressBar1.Maximum = 100;
            progressBar2.Maximum = 100;

            progressBar1.Value = 100;
            progressBar2.Value = 100;

            label4.Text = "100/100";
            label5.Text = "100/100";

            label6.Text = "Choose your action!";

            // Load characters
            LoadCharacterImages();

            // -----------------------------------------------------
            // ATTACK EFFECT PICTURE BOXES
            // -----------------------------------------------------

            // PB3 = SPECIAL ATTACK EFFECT
            pictureBox3.Visible = false;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

            // PB4 = PROJECTILE
            pictureBox4.Visible = false;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }


        // =========================================================
        // CHARACTER IMAGES
        // =========================================================

        private void LoadCharacterImages()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox1.Image = GetCharacterImage(player1Character);
            pictureBox2.Image = GetCharacterImage(player2Character);
        }


        private Image GetCharacterImage(string character)
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


        // =========================================================
        // BASIC ATTACK BUTTON
        // =========================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            BasicAttack();
        }


        // =========================================================
        // BASIC ATTACK
        // PB4 = PROJECTILE
        // =========================================================

        private void BasicAttack()
        {
            isAnimating = true;

            DisableButtons();

            currentAnimation = "BASIC";

            animationStep = 0;

            string attacker =
                player1Turn
                ? player1Character
                : player2Character;

            label6.Text =
                attacker + " used BASIC ATTACK!";

            // Load projectile
            pictureBox4.Image =
                GetBasicAttackImage(attacker);

            pictureBox4.Visible = true;

            // -----------------------------------------------------
            // PLAYER 1 ATTACKING
            // -----------------------------------------------------

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

            // -----------------------------------------------------
            // PLAYER 2 ATTACKING
            // -----------------------------------------------------

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

            pictureBox4.Location = projectileStartPosition;

            animationTimer.Start();
        }


        // =========================================================
        // BASIC ATTACK IMAGE
        // =========================================================

        private Image GetBasicAttackImage(string character)
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


        // =========================================================
        // ANIMATION TIMER
        // =========================================================

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationStep++;

            if (currentAnimation == "BASIC")
            {
                AnimateBasicAttack();
            }

            else if (currentAnimation == "SPECIAL")
            {
                AnimateSpecialAttack();
            }
        }


        // =========================================================
        // BASIC ATTACK ANIMATION
        // =========================================================

        private void AnimateBasicAttack()
        {
            // -----------------------------------------------------
            // MOVE PROJECTILE
            // -----------------------------------------------------

            if (animationStep <= 20)
            {
                MoveProjectile();
            }

            // -----------------------------------------------------
            // HIT
            // -----------------------------------------------------

            else if (animationStep == 21)
            {
                pictureBox4.Visible = false;

                if (player1Turn)
                {
                    DamagePlayer2(20);

                    // Enemy shake
                    pictureBox2.Left += 15;
                }
                else
                {
                    DamagePlayer1(20);

                    // Enemy shake
                    pictureBox1.Left -= 15;
                }
            }

            // -----------------------------------------------------
            // RESET
            // -----------------------------------------------------

            else if (animationStep <= 26)
            {
                if (player1Turn)
                {
                    pictureBox2.Left -= 3;
                }
                else
                {
                    pictureBox1.Left += 3;
                }
            }

            else
            {
                EndAttack();
            }
        }


        // =========================================================
        // MOVE PROJECTILE
        // =========================================================

        private void MoveProjectile()
        {
            int startX = projectileStartPosition.X;
            int startY = projectileStartPosition.Y;

            int targetX = projectileTargetPosition.X;
            int targetY = projectileTargetPosition.Y;

            float progress =
                animationStep / 20f;

            int newX =
                startX +
                (int)((targetX - startX) * progress);

            int newY =
                startY +
                (int)((targetY - startY) * progress);

            pictureBox4.Location =
                new Point(newX, newY);
        }


        // =========================================================
        // SPECIAL ATTACK BUTTON
        // =========================================================

        private void button3_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            SpecialAttack();
        }


        // =========================================================
        // SPECIAL ATTACK
        //
        // PB3 = LARGE SPECIAL EFFECT
        // PB4 = PROJECTILE
        // =========================================================

        private void SpecialAttack()
        {
            isAnimating = true;

            DisableButtons();

            currentAnimation = "SPECIAL";

            animationStep = 0;

            string attacker =
                player1Turn
                ? player1Character
                : player2Character;

            label6.Text =
                attacker + " used SPECIAL ATTACK!";

            // -----------------------------------------------------
            // LOAD SPECIAL EFFECT
            // -----------------------------------------------------

            pictureBox3.Image =
                GetSpecialAttackImage(attacker);

            pictureBox3.Visible = true;

            // -----------------------------------------------------
            // LOAD PROJECTILE
            // -----------------------------------------------------

            pictureBox4.Image =
                GetSpecialProjectileImage(attacker);

            pictureBox4.Visible = false;

            animationTimer.Start();
        }


        // =========================================================
        // SPECIAL ATTACK BACKGROUND EFFECT
        // PB3
        // =========================================================

        private Image GetSpecialAttackImage(string character)
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


        // =========================================================
        // SPECIAL PROJECTILE
        // PB4
        // =========================================================

        private Image GetSpecialProjectileImage(string character)
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


        // =========================================================
        // SPECIAL ATTACK ANIMATION
        // =========================================================

        private void AnimateSpecialAttack()
        {
            // -----------------------------------------------------
            // PHASE 1
            // SPECIAL EFFECT APPEARS
            // -----------------------------------------------------

            if (animationStep <= 25)
            {
                // Keep special effect visible
                pictureBox3.Visible = true;
            }

            // -----------------------------------------------------
            // PHASE 2
            // PROJECTILE STARTS
            // -----------------------------------------------------

            else if (animationStep == 26)
            {
                pictureBox3.Visible = false;

                pictureBox4.Visible = true;

                if (player1Turn)
                {
                    projectileStartPosition = new Point(
                        pictureBox1.Right - 30,
                        pictureBox1.Top +
                        pictureBox1.Height / 2 - 50
                    );

                    projectileTargetPosition = new Point(
                        pictureBox2.Left,
                        pictureBox2.Top +
                        pictureBox2.Height / 2 - 50
                    );
                }
                else
                {
                    projectileStartPosition = new Point(
                        pictureBox2.Left - 100,
                        pictureBox2.Top +
                        pictureBox2.Height / 2 - 50
                    );

                    projectileTargetPosition = new Point(
                        pictureBox1.Right - 30,
                        pictureBox1.Top +
                        pictureBox1.Height / 2 - 50
                    );
                }

                pictureBox4.Location =
                    projectileStartPosition;
            }

            // -----------------------------------------------------
            // PHASE 3
            // PROJECTILE MOVES
            // -----------------------------------------------------

            else if (animationStep <= 46)
            {
                MoveSpecialProjectile();
            }

            // -----------------------------------------------------
            // PHASE 4
            // HIT
            // -----------------------------------------------------

            else if (animationStep == 47)
            {
                pictureBox4.Visible = false;

                if (player1Turn)
                {
                    DamagePlayer2(35);

                    pictureBox2.Left += 20;
                }
                else
                {
                    DamagePlayer1(35);

                    pictureBox1.Left -= 20;
                }
            }

            // -----------------------------------------------------
            // PHASE 5
            // RETURN / FINISH
            // -----------------------------------------------------

            else if (animationStep <= 52)
            {
                if (player1Turn)
                {
                    pictureBox2.Left -= 4;
                }
                else
                {
                    pictureBox1.Left += 4;
                }
            }

            else
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;

                EndAttack();
            }
        }


        // =========================================================
        // MOVE SPECIAL PROJECTILE
        // =========================================================

        private void MoveSpecialProjectile()
        {
            int startX = projectileStartPosition.X;
            int startY = projectileStartPosition.Y;

            int targetX = projectileTargetPosition.X;
            int targetY = projectileTargetPosition.Y;

            float progress =
                (animationStep - 26) / 20f;

            if (progress > 1)
                progress = 1;

            int newX =
                startX +
                (int)((targetX - startX) * progress);

            int newY =
                startY +
                (int)((targetY - startY) * progress);

            pictureBox4.Location =
                new Point(newX, newY);
        }


        // =========================================================
        // DAMAGE PLAYER 2
        // =========================================================

        private void DamagePlayer2(int damage)
        {
            player2HP -= damage;

            if (player2HP < 0)
                player2HP = 0;

            progressBar2.Value = player2HP;

            label5.Text =
                player2HP + "/100";

            label6.Text =
                player2Character +
                " took " +
                damage +
                " damage!";
        }


        // =========================================================
        // DAMAGE PLAYER 1
        // =========================================================

        private void DamagePlayer1(int damage)
        {
            player1HP -= damage;

            if (player1HP < 0)
                player1HP = 0;

            progressBar1.Value = player1HP;

            label4.Text =
                player1HP + "/100";

            label6.Text =
                player1Character +
                " took " +
                damage +
                " damage!";
        }


        // =========================================================
        // END ATTACK
        // =========================================================

        private void EndAttack()
        {
            animationTimer.Stop();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            // Restore characters
            pictureBox1.Location =
                player1StartPosition;

            pictureBox2.Location =
                player2StartPosition;

            // Check winner
            if (player1HP <= 0)
            {
                EndBattle(player2Character);
                return;
            }

            if (player2HP <= 0)
            {
                EndBattle(player1Character);
                return;
            }

            // Change turn
            player1Turn = !player1Turn;

            UpdateTurn();

            isAnimating = false;

            EnableButtons();
        }


        // =========================================================
        // DEFEND
        // =========================================================

        private void button2_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            string character =
                player1Turn
                ? player1Character
                : player2Character;

            label6.Text =
                character +
                " is defending!";

            player1Turn = !player1Turn;

            UpdateTurn();
        }


        // =========================================================
        // UPDATE TURN
        // =========================================================

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

            label6.Text =
                "Choose your action!";
        }


        // =========================================================
        // BUTTON CONTROL
        // =========================================================

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
            button3.Enabled = true;
        }


        // =========================================================
        // END BATTLE
        // =========================================================

        private void EndBattle(string winner)
        {
            animationTimer.Stop();

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;

            DisableButtons();

            label3.Text =
                "BATTLE OVER";

            label6.Text =
                winner.ToUpper() +
                " WINS!";

            MessageBox.Show(
                winner + " wins!",
                "Battle Finished",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }


        // =========================================================
        // DESIGNER EVENTS
        // =========================================================

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void BattleForm_Load(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}
