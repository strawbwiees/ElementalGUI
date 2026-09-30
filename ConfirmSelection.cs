using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElementalGUI
{
    public partial class ConfirmSelection : Form
    {
        private string player1Character;
        private string player2Character;

        // set when the player hits CONFIRM

        public bool Confirmed { get; private set; } = false;

        public ConfirmSelection(string player1, string player2)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            player1Character = player1;
            player2Character = player2;

            label1.Text = "CONFIRM SELECTION";

            label2.Text = "PLAYER 1";
            label3.Text = "PLAYER 2";

            label4.Text = player1Character.ToUpper();
            label5.Text = player2Character.ToUpper();

            label6.Text = "Are you ready?";

            button1.Text = "CANCEL";
            button2.Text = "CONFIRM";

            CartoonUI.StyleButton(button1, CartoonUI.BadColor);
            CartoonUI.StyleButton(button2, CartoonUI.GoodColor);
            button1.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button1);
            button2.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CartoonUI.PlayClick();
            Confirmed = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CartoonUI.PlayClick();
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
