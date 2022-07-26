using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Timer
{
    public class ProgressBarEx : ProgressBar
    {
        public ProgressBarEx()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            Brush color = Brushes.Pink;
            e.Graphics.FillRectangle(color, 2, 2, rec.Width, rec.Height);

        }
    }
}
