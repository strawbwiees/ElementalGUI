using System;
using System.Windows.Forms;
using static ElementalGUI.gameData;

namespace ElementalGUI
{
    public partial class ChooseCharacters : Form
    {
        private int currentPlayer = 1;
        private Character selectedCharacter = null;

        public ChooseCharacters()
        {
            InitializeComponent();

            label1.Text = "CHOOSE YOUR CHARACTER";
            label2.Text = "Player 1";
        }

        // ==========================================
        // FIRE / LUMEN
        // ==========================================

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character(
                "Lumen",
                "Fire",
                20
            );
        }

        // ==========================================
        // WATER / RIPPLE
        // ==========================================

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character(
                "Ripple",
                "Water",
                20
            );
        }

        // ==========================================
        // EARTH / GRUNCHWOOD
        // ==========================================

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character(
                "Grunchwood",
                "Earth",
                20
            );
        }

        // ==========================================
        // AIR / GALE
        // ==========================================

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character(
                "Gale",
                "Air",
                20
            );
        }

        // ==========================================
        // SELECT BUTTON
        // ==========================================

        private void select_Click(object sender, EventArgs e)
        {
            if (selectedCharacter == null)
            {
                MessageBox.Show(
                    "Please choose a character first!",
                    "No Character Selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // PLAYER 1
            if (currentPlayer == 1)
            {
                GameData.Player1 = selectedCharacter;

                currentPlayer = 2;

                label2.Text = "Player 2";

                // Clear selection
                selectedCharacter = null;
            }

            // PLAYER 2
            else
            {
                GameData.Player2 = selectedCharacter;

                Battle battleForm = new Battle();

                battleForm.Show();

                this.Hide();
            }
        }

        // ==========================================
        // BACK BUTTON
        // ==========================================

        private void back_Click(object sender, EventArgs e)
        {
            MainMenu mainMenuForm = new MainMenu();

            mainMenuForm.Show();

            this.Hide();
        }

        // ==========================================
        // LABEL EVENTS
        // ==========================================

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void Water_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}