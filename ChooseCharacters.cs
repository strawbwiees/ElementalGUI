
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

        // FIRE
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character("Lumen", "Fire", 20);

            MessageBox.Show("Lumen selected!");
        }

        // WATER
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character("Ripple", "Water", 20);

            MessageBox.Show("Ripple selected!");
        }

        // EARTH
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character("Grunchwood", "Earth", 20);

            MessageBox.Show("Grunchwood selected!");
        }

        // AIR
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            selectedCharacter = new Character("Gale", "Air", 20);

            MessageBox.Show("Gale selected!");
        }

        // SELECT BUTTON
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
                selectedCharacter = null;

                label2.Text = "Player 2";

                MessageBox.Show(
                    "Player 1 selected " + GameData.Player1.GetName() +
                    "!\n\nNow it's Player 2's turn.",
                    "Player 1 Selected"
                );
            }

            // PLAYER 2
            else
            {
                GameData.Player2 = selectedCharacter;

                MessageBox.Show(
                    "Player 2 selected " + GameData.Player2.GetName() +
                    "!",
                    "Player 2 Selected"
                );

                Battle battleForm = new Battle();
                battleForm.Show();

                this.Hide();
            }
        }

        // BACK BUTTON
        private void back_Click(object sender, EventArgs e)
        {
            MainMenu mainMenuForm = new MainMenu();
            mainMenuForm.Show();

            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}
```
