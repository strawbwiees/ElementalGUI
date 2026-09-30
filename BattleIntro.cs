using System;
using System.Drawing;
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

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Get Ready!";
            this.DoubleBuffered = true;

            player1Character = player1;
            player2Character = player2;

            label1.Text = player1Character.ToUpper();
            label2.Text = player2Character.ToUpper();

            label3.Text = "VS";

            LoadCharacterImages();

            this.FormClosing += BattleIntro_FormClosing;

            // both fighters on the shared sprite clock
            SpriteAnimator.SetSprite(pictureBox1, pictureBox1.Image);
            SpriteAnimator.SetSprite(pictureBox2, pictureBox2.Image);

            var spriteClock = new System.Windows.Forms.Timer { Interval = 33 };
            spriteClock.Tick += (s, e) => SpriteAnimator.Tick();
            spriteClock.Start();

            CartoonUI.StyleButton(button1, CartoonUI.GoldColor);
            button1.ForeColor = CartoonUI.InkColor;
            button1.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button1);
        }

        private void LoadCharacterImages()
        {
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

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            CartoonUI.PlayClick();

            GameFlow.NavigateTo(
                new BattleForm(
                    player1Character,
                    player2Character
                )
            );
        }

        // X goes back to character select
        private void BattleIntro_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !GameFlow.Navigating)
            {
                GameFlow.NavigateTo(new ChooseCharacter());
            }
        }

        private void label1_Click(object? sender, EventArgs e)
        {
        }

        private void label2_Click(object? sender, EventArgs e)
        {
        }

        private void label3_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object? sender, EventArgs e)
        {
        }
    }
}
