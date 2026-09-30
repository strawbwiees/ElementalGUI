
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class ConfirmSelection : Form
    {
        private string player1Character;
        private string player2Character;

        public bool Confirmed { get; private set; } = false;

        public ConfirmSelection(string player1, string player2)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Store the selected characters
            player1Character = player1;
            player2Character = player2;

            // Title
            label1.Text = "CONFIRM SELECTION";

            // Players
            label2.Text = "PLAYER 1";
            label3.Text = "PLAYER 2";

            // Characters
            label4.Text = player1Character.ToUpper();
            label5.Text = player2Character.ToUpper();

            // Question
            label6.Text = "Are you ready?";

            // Buttons
            button1.Text = "CANCEL";
            button2.Text = "CONFIRM";
        }

        // CANCEL
        private void button1_Click(object sender, EventArgs e)
        {
            Confirmed = false;
            this.Close();
        }

        // CONFIRM
        private void button2_Click(object sender, EventArgs e)
        {
            Confirmed = true;
            this.Close();
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

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void ConfirmSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
