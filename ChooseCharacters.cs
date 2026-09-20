
using System;
using System.Drawing;
using System.Windows.Forms;
using static ElementalGUI.gameData;

namespace ElementalGUI
{
    public partial class ChooseCharacters : Form
    {
        // ==========================================
        // VARIABLES
        // ==========================================

        private int currentPlayer = 1;

        private Character selectedCharacter = null;

        private PictureBox selectedPicture = null;


        // ==========================================
        // CONSTRUCTOR
        // ==========================================

        public ChooseCharacters()
        {
            InitializeComponent();

            // Initial screen
            label1.Text = "CHOOSE YOUR CHARACTER";
            label2.Text = "Player 1";
        }


        // ==========================================
        // FIRE - LUMEN
        // ==========================================

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectCharacter(
                new Character("Lumen", "Fire", 20),
                sender as PictureBox
            );
        }


        // ==========================================
        // WATER - RIPPLE
        // ==========================================

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SelectCharacter(
                new Character("Ripple", "Water", 20),
                sender as PictureBox
            );
        }


        // ==========================================
        // EARTH - GRUNCHWOOD
        // ==========================================

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SelectCharacter(
                new Character("Grunchwood", "Earth", 20),
                sender as PictureBox
            );
        }


        // ==========================================
        // AIR - GALE
        // ==========================================

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SelectCharacter(
                new Character("Gale", "Air", 20),
                sender as PictureBox
            );
        }


        // ==========================================
        // SELECT CHARACTER
        // ==========================================

        private void SelectCharacter(
            Character character,
            PictureBox picture)
        {
            // Save selected character
            selectedCharacter = character;


            // Remove previous highlight
            if (selectedPicture != null)
            {
                selectedPicture.BorderStyle =
                    BorderStyle.None;
            }


            // Save the newly clicked picture
            selectedPicture = picture;


            // Highlight the newly selected picture
            if (selectedPicture != null)
            {
                selectedPicture.BorderStyle =
                    BorderStyle.Fixed3D;
            }
        }


        // ==========================================
        // SELECT BUTTON
        // ==========================================

        private void select_Click(object sender, EventArgs e)
        {
            // --------------------------------------
            // No character selected
            // --------------------------------------

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


            // ======================================
            // PLAYER 1
            // ======================================

            if (currentPlayer == 1)
            {
                // Save Player 1
                GameData.Player1 = selectedCharacter;


                // Move to Player 2
                currentPlayer = 2;


                // Change the label
                label2.Text = "Player 2";


                // Clear selected character
                selectedCharacter = null;


                // Remove Player 1's highlight
                if (selectedPicture != null)
                {
                    selectedPicture.BorderStyle =
                        BorderStyle.None;

                    selectedPicture = null;
                }
            }


            // ======================================
            // PLAYER 2
            // ======================================

            else
            {
                // Save Player 2
                GameData.Player2 = selectedCharacter;


                // Open Battle
                Battle battleForm = new Battle();

                battleForm.Show();


                // Hide Character Selection
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


        // ==========================================
        // WATER EVENT
        // ==========================================

        private void Water_Click(object sender, EventArgs e)
        {
        }
    }
}

