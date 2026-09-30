using System.Windows.Forms;

namespace ElementalGUI
{
    internal static class GameFlow
    {
        // blocks FormClosing handlers during a screen swap
        public static bool Navigating = false;

        // close other windows, show the next one (menu stays as anchor)
        public static void NavigateTo(Form next)
        {
            Navigating = true;

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Form form = Application.OpenForms[i]!;

                if (form is MainMenu || form == next || form.IsDisposed)
                    continue;

                form.Close();
            }

            next.Show();
            next.StartPosition = FormStartPosition.CenterScreen;

            Navigating = false;
        }

        public static void QuitGame()
        {
            Application.Exit();
        }

        // back to main menu from any screen
        public static void ShowMenu()
        {
            Navigating = true;

            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                Form form = Application.OpenForms[i]!;

                if (form is MainMenu || form.IsDisposed)
                    continue;

                form.Close();
            }

            foreach (Form form in Application.OpenForms)
            {
                if (form is MainMenu menu)
                {
                    menu.Show();
                    menu.BringToFront();
                    break;
                }
            }

            Navigating = false;
        }
    }
}
