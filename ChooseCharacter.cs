using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class ChooseCharacter : Form
    {
        // Stores the character currently selected
        private PictureBox selectedCharacter = null;

        // Stores the characters chosen by each player
        private string player1Character = "";
        private string player2Character = "";

        // Keeps track of whose turn it is
        private int currentPlayer = 1;

        public ChooseCharacter()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Give each character a name
            pictureBox1.Tag = "Lumen";
            pictureBox2.Tag = "Ripple";
            pictureBox3.Tag = "Grunch";
            pictureBox4.Tag = "Gale";

            // Make the characters clickable
            pictureBox1.Click += Character_Click;
            pictureBox2.Click += Character_Click;
            pictureBox3.Click += Character_Click;
            pictureBox4.Click += Character_Click;

            // Allow us to draw the selection outline
            pictureBox1.Paint += Character_Paint;
            pictureBox2.Paint += Character_Paint;
            pictureBox3.Paint += Character_Paint;
            pictureBox4.Paint += Character_Paint;

            // DO NOT add:
            // button1.Click += btnSelect_Click;
            //
            // The button is already connected to button1_Click
            // through the Designer.
        }

        // Runs whenever a character is clicked
        private void Character_Click(object sender, EventArgs e)
        {
            selectedCharacter = sender as PictureBox;

            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
        }

        // Draws the outline around the selected character
        private void Character_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox == selectedCharacter)
            {
                using (Pen pen = new Pen(Color.White, 5))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        2,
                        2,
                        pictureBox.Width - 5,
                        pictureBox.Height - 5
                    );
                }
            }
        }

        // SELECT BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            // Make sure a character was selected
            if (selectedCharacter == null)
            {
                MessageBox.Show("Please choose a character first.");
                return;
            }

            // PLAYER 1
            if (currentPlayer == 1)
            {
                player1Character = selectedCharacter.Tag.ToString();

                currentPlayer = 2;

                PreparePlayer2();
            }
            // PLAYER 2
            else
            {
                player2Character = selectedCharacter.Tag.ToString();

                StartGame();
            }
        }

        // Change to Player 2
        private void PreparePlayer2()
        {
            label2.Text = "PLAYER 2";

            // Clear current selection
            selectedCharacter = null;

            // Prevent Player 2 from choosing Player 1's character
            pictureBox1.Enabled = player1Character != "Lumen";
            pictureBox2.Enabled = player1Character != "Ripple";
            pictureBox3.Enabled = player1Character != "Grunch";
            pictureBox4.Enabled = player1Character != "Gale";

            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
        }

        // Open confirmation after Player 2 selects
        private void StartGame()
        {
            ConfirmSelection confirmation =
                new ConfirmSelection(
                    player1Character,
                    player2Character
                );

            confirmation.ShowDialog(this);

            // Only continue if CONFIRM was clicked
            if (confirmation.Confirmed)
            {
                BattleIntro battleIntro =
                    new BattleIntro(
                        player1Character,
                        player2Character
                    );

                this.Hide();

                battleIntro.ShowDialog();

                this.Show();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
        }
    }
}