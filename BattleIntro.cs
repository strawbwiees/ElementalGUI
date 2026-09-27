
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class BattleIntro : Form
    {
        private string player1Character;
        private string player2Character;

        public BattleIntro(string player1, string player2)
        {
            InitializeComponent();

            player1Character = player1;
            player2Character = player2;

            // Display character names
            label1.Text = player1Character.ToUpper();
            label2.Text = player2Character.ToUpper();

            // VS
            label3.Text = "VS";

            // Load character images
            LoadCharacterImages();
        }

        private void LoadCharacterImages()
        {
            // Player 1
            if (player1Character == "Lumen")
            {
                pictureBox1.Image = Properties.Resources.fire;
            }
            else if (player1Character == "Ripple")
            {
                pictureBox1.Image = Properties.Resources.water;
            }
            else if (player1Character == "Grunch")
            {
                pictureBox1.Image = Properties.Resources.earth;
            }
            else if (player1Character == "Gale")
            {
                pictureBox1.Image = Properties.Resources.wind;
            }

            // Player 2
            if (player2Character == "Lumen")
            {
                pictureBox2.Image = Properties.Resources.fire;
            }
            else if (player2Character == "Ripple")
            {
                pictureBox2.Image = Properties.Resources.water;
            }
            else if (player2Character == "Grunch")
            {
                pictureBox2.Image = Properties.Resources.earth;
            }
            else if (player2Character == "Gale")
            {
                pictureBox2.Image = Properties.Resources.wind;
            }

            // Make images fit their PictureBoxes
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // START BATTLE
        private void button1_Click(object sender, EventArgs e)
        {
            // Put your actual Battle Form here later.

            MessageBox.Show("Battle Starting!");
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
        }

        // pictureBox3 is your GAME LOGO.
        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }
    }
}
