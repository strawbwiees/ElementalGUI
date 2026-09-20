namespace ElementalGUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void play(object sender, EventArgs e)
        {
            ChooseCharacters chooseCharactersForm = new ChooseCharacters();
            chooseCharactersForm.Show();
            this.Hide();
        }

        private void exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
