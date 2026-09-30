using System;
using System.Drawing;
using System.Windows.Forms;


namespace ElementalGUI
{
    public partial class ChooseCharacter : Form
    {
        // currently highlighted card
        private PictureBox? selectedCharacter = null;

        private string player1Character = "";
        private string player2Character = "";

        // picks per player + whose turn to choose
        private int currentPlayer = 1;

        public ChooseCharacter()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Choose Your Character";
            this.DoubleBuffered = true;

            pictureBox1.Tag = "Lumen";
            pictureBox2.Tag = "Ripple";
            pictureBox3.Tag = "Grunch";
            pictureBox4.Tag = "Gale";

            pictureBox1.Click += Character_Click;
            pictureBox2.Click += Character_Click;
            pictureBox3.Click += Character_Click;
            pictureBox4.Click += Character_Click;

            // sprites first so the ring paints on top
            SpriteAnimator.SetSprite(pictureBox1, pictureBox1.Image);
            SpriteAnimator.SetSprite(pictureBox2, pictureBox2.Image);
            SpriteAnimator.SetSprite(pictureBox3, pictureBox3.Image);
            SpriteAnimator.SetSprite(pictureBox4, pictureBox4.Image);

            var spriteClock = new System.Windows.Forms.Timer { Interval = 33 };
            spriteClock.Tick += (s, e) => SpriteAnimator.Tick();
            spriteClock.Start();

            pictureBox1.Paint += Character_Paint;
            pictureBox2.Paint += Character_Paint;
            pictureBox3.Paint += Character_Paint;
            pictureBox4.Paint += Character_Paint;

            CartoonUI.StyleButton(button1, CartoonUI.GoodColor);
            button1.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button1);

            label2.ForeColor = Color.FromArgb(126, 187, 255);

            this.FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing && !GameFlow.Navigating)
                {
                    GameFlow.ShowMenu();
                }
            };
        }

        private void Character_Click(object? sender, EventArgs e)
        {
            CartoonUI.PlayClick();

            selectedCharacter = sender as PictureBox;

            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
        }

        // selection ring in the current player's color
        private void Character_Paint(object? sender, PaintEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender!;

            if (pictureBox == selectedCharacter)
            {
                Color ring = currentPlayer == 1
                    ? Color.FromArgb(74, 144, 217)   // Player 1 blue
                    : CartoonUI.BadColor;            // Player 2 red

                using (Pen pen = new Pen(ring, 6))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        3,
                        3,
                        pictureBox.Width - 7,
                        pictureBox.Height - 7
                    );
                }
            }
        }

        // lock in the pick, then hand off to player 2
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedCharacter == null)
            {
                CartoonUI.PlayBlock();
                MessageBox.Show("Please choose a character first.");
                return;
            }

            if (currentPlayer == 1)
            {
                player1Character = selectedCharacter.Tag!.ToString()!;

                currentPlayer = 2;

                PreparePlayer2();
            }
            else
            {
                player2Character = selectedCharacter.Tag!.ToString()!;

                StartGame();
            }
        }

        // player 2 can't take player 1's pick
        private void PreparePlayer2()
        {
            label2.Text = "PLAYER 2";
            label2.ForeColor = CartoonUI.BadColor;

            selectedCharacter = null;

            pictureBox1.Enabled = player1Character != "Lumen";
            pictureBox2.Enabled = player1Character != "Ripple";
            pictureBox3.Enabled = player1Character != "Grunch";
            pictureBox4.Enabled = player1Character != "Gale";

            pictureBox1.Invalidate();
            pictureBox2.Invalidate();
            pictureBox3.Invalidate();
            pictureBox4.Invalidate();
        }

        private void StartGame()
        {
            ConfirmSelection confirmation =
                new ConfirmSelection(
                    player1Character,
                    player2Character
                );

            confirmation.ShowDialog(this);

            if (confirmation.Confirmed)
            {
                GameFlow.NavigateTo(
                    new BattleIntro(
                        player1Character,
                        player2Character
                    )
                );
            }
        }

        private void label2_Click(object? sender, EventArgs e)
        {
        }

        private void label1_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object? sender, EventArgs e)
        {
        }

        private void pictureBox4_Click(object? sender, EventArgs e)
        {
        }
    }
}
