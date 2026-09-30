namespace ElementalGUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ElementalGUI";

            CartoonUI.StyleButton(button1, CartoonUI.GoodColor);
            CartoonUI.StyleButton(button2, CartoonUI.BadColor); 

            button1.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button1);
            button2.Paint += (s, e) => CartoonUI.DrawButtonOutline(e, button2);

            this.FormClosing += Form1_FormClosing;
        }

        private void play(object? sender, EventArgs e)
        {
            CartoonUI.PlayClick();
            GameFlow.NavigateTo(new ChooseCharacter());
        }

        private void Form1_Load(object? sender, EventArgs e)
        {

        }

        private void button2_Click(object? sender, EventArgs e)
        {
            CartoonUI.PlayClick();
            Application.Exit();
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!GameFlow.Navigating)
            {
                Application.Exit();
            }
        }
    }
}
