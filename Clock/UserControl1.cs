using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class UCClock: UserControl
    {
        public UCClock()
        {
            InitializeComponent();
        }

        int hour = 0;

        public int Hour
        {
            get { return hour; }
            set { hour = value; this.Invalidate(); }
        }
        int minute = 15;

        public int Minute
        {
            get { return minute; }
            set { minute = value; this.Invalidate(); }
        }
        int second = 30;

        public int Second
        {
            get { return second; }
            set { second = value; this.Invalidate(); }
        }

        private void UCClock_Paint(object sender, PaintEventArgs e)
        {
            int clockR = 0;
            if (this.Height > this.Width)
            {
                clockR = this.Width - 10;
            }
            else
            {
                clockR = this.Height - 10;
            }

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //画刻线
            int rKeDu = clockR / 2 * 1 / 12;
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                    g.FillRectangle(Brushes.Black, -1, -clockR / 2, 2, rKeDu);
                else
                    g.FillRectangle(Brushes.Gray, -1, -clockR / 2, 2, rKeDu / 2);
                g.RotateTransform(6);
            }
            g.TranslateTransform(-this.Width / 2, -this.Height / 2);

            //画数字
            int r = clockR / 2 * 4 / 5;
            Font font = new System.Drawing.Font("微软雅黑", rKeDu);
            Brush brush = Brushes.Black;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(1 + "", font, brush, this.Width / 2 + r / 2, this.Height / 2 - (int)(r / 2 * Math.Sqrt(3)), sf);
            g.DrawString(2 + "", font, brush, this.Width / 2 + (int)(r / 2 * Math.Sqrt(3)), this.Height / 2 - r / 2, sf);
            g.DrawString(3 + "", font, brush, this.Width / 2 + r, this.Height / 2, sf);
            g.DrawString(4 + "", font, brush, this.Width / 2 + (int)(r / 2 * Math.Sqrt(3)), this.Height / 2 + r / 2, sf);
            g.DrawString(5 + "", font, brush, this.Width / 2 + r / 2, this.Height / 2 + (int)(r / 2 * Math.Sqrt(3)), sf);
            g.DrawString(6 + "", font, brush, this.Width / 2, this.Height / 2 + r, sf);
            g.DrawString(7 + "", font, brush, this.Width / 2 - r / 2, this.Height / 2 + (int)(r / 2 * Math.Sqrt(3)), sf);
            g.DrawString(8 + "", font, brush, this.Width / 2 - (int)(r / 2 * Math.Sqrt(3)), this.Height / 2 + r / 2, sf);
            g.DrawString(9 + "", font, brush, this.Width / 2 - r, this.Height / 2, sf);
            g.DrawString(10 + "", font, brush, this.Width / 2 - (int)(r / 2 * Math.Sqrt(3)), this.Height / 2 - r / 2, sf);
            g.DrawString(11 + "", font, brush, this.Width / 2 - r / 2, this.Height / 2 - (int)(r / 2 * Math.Sqrt(3)), sf);
            g.DrawString(12 + "", font, brush, this.Width / 2, this.Height / 2 - r, sf);

            int bankuan = clockR / 100;
            if (bankuan < 4)
                bankuan = 4;
            //画时针
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.RotateTransform(hour * 30 + 30*minute/60);
            g.FillPath(Brushes.Black, CreateRoundedRectanglePath(new Rectangle(-bankuan, -clockR / 2 + 75, bankuan*2, clockR / 2 - 70), 5));
            g.FillPath(Brushes.White, CreateRoundedRectanglePath(new Rectangle(-(bankuan - 2), -clockR / 2 + 77, (bankuan - 2) * 2, (clockR / 2 - 70)/3), 5));
            g.ResetTransform();
            //画分针
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.RotateTransform(minute * 6 + 6*second/60);
            g.FillPath(Brushes.Black, CreateRoundedRectanglePath(new Rectangle(-(bankuan - 1), -clockR / 2 + 35, (bankuan - 1)*2, clockR / 2 - 30), 5));
            g.FillPath(Brushes.White, CreateRoundedRectanglePath(new Rectangle(-(bankuan - 3), -clockR / 2 + 37, (bankuan - 3) * 2, (clockR / 2 - 30) / 5), 5));
            g.ResetTransform();
            //画秒针
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.RotateTransform(second * 6);
            g.FillRectangle(Brushes.DodgerBlue, -1, -clockR / 2, 2, clockR / 2 + 16);
            g.FillEllipse(Brushes.DodgerBlue, -8, -8, 16, 16);
            g.FillEllipse(Brushes.White, -6, -6, 12, 12);
            g.ResetTransform();
            
        }

        public GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        private void UCClock_Load(object sender, EventArgs e)
        {
            
        }
    }
}
