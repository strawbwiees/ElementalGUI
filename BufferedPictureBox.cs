using System.Windows.Forms;

namespace ElementalGUI
{
    // double buffered, no flicker on moves
    public class BufferedPictureBox : PictureBox
    {
        public BufferedPictureBox()
        {
            DoubleBuffered = true;
        }
    }
}
