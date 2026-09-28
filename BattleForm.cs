using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class BattleForm : Form
    {
        // ==============================
        // CHARACTER DATA
        // ==============================

        private string player1Character;
        private string player2Character;

        // ==============================
        // HP
        // ==============================

        private int player1HP = 100;
        private int player2HP = 100;

        // ==============================
        // TURN
        // ==============================

        private bool player1Turn = true;

        // ==============================
        // ANIMATION
        // ==============================

        private System.Windows.Forms.Timer animationTimer;
        private int animationStep = 0;

        private bool isAnimating = false;

        private Point player1StartPosition;
        private Point player2StartPosition;


        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public BattleForm(string player1, string player2)
        {
            InitializeComponent();

            player1Character = player1;
            player2Character = player2;

            // Remember starting positions
            player1StartPosition = pictureBox1.Location;
            player2StartPosition = pictureBox2.Location;

            // Setup timer
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Interval = 20;
            animationTimer.Tick += AnimationTimer_Tick;

            SetupBattle();
        }


        // =========================================================
        // SETUP
        // =========================================================

        private void SetupBattle()
        {
            // Character names
            label1.Text = player1Character.ToUpper();
            label2.Text = player2Character.ToUpper();

            // Turn
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

            // Battle message
            label6.Text = "Choose your action!";

            // Load characters
            LoadCharacterImages();

            // Attack effect is hidden for now
            pictureBox3.Visible = false;
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
        // ATTACK BUTTON
        // =========================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            BasicAttack();
        }


        // =========================================================
        // BASIC ATTACK
        // =========================================================

        private void BasicAttack()
        {
            isAnimating = true;

            DisableButtons();

            if (player1Turn)
            {
                label6.Text =
                    player1Character + " used basic attack!";

                animationStep = 0;
                animationTimer.Tag = "PLAYER1";
            }
            else
            {
                label6.Text =
                    player2Character + " used basic attack!";

                animationStep = 0;
                animationTimer.Tag = "PLAYER2";
            }

            animationTimer.Start();
        }


        // =========================================================
        // ANIMATION TIMER
        // =========================================================

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationStep++;

            if (animationTimer.Tag.ToString() == "PLAYER1")
            {
                AnimatePlayer1Attack();
            }
            else
            {
                AnimatePlayer2Attack();
            }
        }


        // =========================================================
        // PLAYER 1 ATTACK
        // =========================================================

        private void AnimatePlayer1Attack()
        {
            // Move toward Player 2
            if (animationStep <= 10)
            {
                pictureBox1.Left += 8;
            }

            // Hit
            else if (animationStep == 11)
            {
                DamagePlayer2(20);

                // Shake Player 2
                pictureBox2.Left += 15;
            }

            // Return
            else if (animationStep <= 21)
            {
                pictureBox1.Left -= 8;
            }

            // Finish
            else
            {
                EndAttack();
            }
        }


        // =========================================================
        // PLAYER 2 ATTACK
        // =========================================================

        private void AnimatePlayer2Attack()
        {
            // Move toward Player 1
            if (animationStep <= 10)
            {
                pictureBox2.Left -= 8;
            }

            // Hit
            else if (animationStep == 11)
            {
                DamagePlayer1(20);

                // Shake Player 1
                pictureBox1.Left -= 15;
            }

            // Return
            else if (animationStep <= 21)
            {
                pictureBox2.Left += 8;
            }

            // Finish
            else
            {
                EndAttack();
            }
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

            label5.Text = player2HP + "/100";

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

            label4.Text = player1HP + "/100";

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

            // Restore original positions
            pictureBox1.Location = player1StartPosition;
            pictureBox2.Location = player2StartPosition;

            // Check if someone died
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
        // UPDATE TURN
        // =========================================================

        private void UpdateTurn()
        {
            if (player1Turn)
            {
                label3.Text =
                    player1Character.ToUpper() + "'S TURN";
            }
            else
            {
                label3.Text =
                    player2Character.ToUpper() + "'S TURN";
            }

            label6.Text = "Choose your action!";
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
                character + " is defending!";

            player1Turn = !player1Turn;

            UpdateTurn();
        }


        // =========================================================
        // SPECIAL ATTACK
        // =========================================================

        private void button3_Click(object sender, EventArgs e)
        {
            if (isAnimating)
                return;

            isAnimating = true;

            DisableButtons();

            string attacker =
                player1Turn
                ? player1Character
                : player2Character;

            label6.Text =
                attacker + " used SPECIAL ATTACK!";

            // Special attack = 35 damage
            if (player1Turn)
            {
                DamagePlayer2(35);
            }
            else
            {
                DamagePlayer1(35);
            }

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

            player1Turn = !player1Turn;

            UpdateTurn();

            isAnimating = false;

            EnableButtons();
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

            DisableButtons();

            label3.Text = "BATTLE OVER";

            label6.Text =
                winner.ToUpper() + " WINS!";

            MessageBox.Show(
                winner + " wins!",
                "Battle Finished",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }


        // =========================================================
        // EMPTY DESIGNER EVENTS
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

        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}