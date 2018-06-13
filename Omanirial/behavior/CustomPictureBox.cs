using Omanirial.data;
using Omanirial.util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Omanirial.behavior
{
    public partial class CustomPictureBox : PictureBox
    {
        #region Paint
        private float CalcScale(Graphics g)
        {
            var bounds = g.VisibleClipBounds;
            var scaleH = bounds.Width / Page.Width;
            var scaleV = bounds.Height / Page.Height;

            return Math.Min(scaleH, scaleV);
        }

        private void DrawCell(Graphics g, float scale)
        {
            if (!MousePt.HasValue)
            {
                return;
            }
            var topMargin = (Height / scale - Page.Height) / 2;
            var leftMargin = (Width / scale - Page.Width) / 2;
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;
            var mx = (int)(MousePt.Value.X / scale - leftMargin);
            var my = (int)(MousePt.Value.Y / scale - topMargin);
            var min = int.MaxValue;

            foreach (var mark in Page.MarkList)
            {
                var pt = mark.Location;
                var dist = Math.Pow(pt.X - mx, 2) + Math.Pow(pt.Y - my, 2);

                if (dist < min)
                {
                    LastMark = mark;
                    min = (int)dist;
                }
            }
            if (LastMark != null)
            {
                var p = LastMark.Location;

                g.DrawRectangle(Pens.Green, new Rectangle(p.X - r, p.Y - r, w, w));
                DrawScore(g, LastMark, Brushes.DarkBlue);
            }
        }

        private void DrawScore(Graphics g, MarkInfo mark, Brush brush)
        {
            var score = mark.Score;

            if (score == 0)
            {
                return;
            }
            var pref = Preference.Instance;
            var pt = mark.Location;
            var r = pref.MarkRadius;

            using (var font = new Font(FontFamily.GenericMonospace, 14))
            {
                g.DrawString(score.ToString(), font, brush, new PointF(pt.X - r, pt.Y - r));
            }
        }

        private void DrawMarks(Graphics g)
        {
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in Page.MarkList)
            {
                var pt = mark.Location;

                if (500 < mark.Score)
                {
                    g.DrawEllipse(Pens.LimeGreen, new Rectangle(pt.X - r, pt.Y - r, w, w));
                }
                if (ShowScore)
                {
                    DrawScore(g, mark, Brushes.MediumPurple);
                }
            }
        }

        private void DrawGrid(Graphics g, float scale)
        {
            var left = Page.Width;
            var right = 0;
            var y = Page.MarkAreaBottom - (Page.MarkAreaRows - 1) * Page.MarkPitch;

            using (var pen = new Pen(Brushes.LightSkyBlue, .2f))
            {
                foreach (var pt in Page.TimingMarkList)
                {
                    g.DrawLine(pen, new Point(pt.X, y), pt);
                    left = Math.Min(left, pt.X);
                    right = Math.Max(right, pt.X);
                }

                for (var ix = 0; ix < Page.MarkAreaRows; ix++)
                {
                    g.DrawLine(pen, new Point(left, y), new Point(right, y));
                    y += Page.MarkPitch;
                }
            }
            g.FillEllipse(Brushes.Red, new Rectangle(Page.Width / 2 - 3, Page.TimingMarkTop - 3, 6, 6));
        }

        private void DrawHist(Graphics g)
        {
            if (LastMark == null)
            {
                return;
            }
            var x = 0;
            var max = (int)(LastMark.Hist.Length * .65);

            foreach (var b in LastMark.Hist)
            {
                if (x < max)
                {
                    g.DrawLine(Pens.OrangeRed, new Point(x, 0), new Point(x, b));
                }
                else
                {
                    g.DrawLine(Pens.Gray, new Point(x, 0), new Point(x, b));
                }
                x += 1;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Page == null)
            {
                return;
            }
            var g = e.Graphics;
            var state = g.Save();
            var scale = CalcScale(g);
            var topMargin = (Height / scale - Page.Height) / 2;
            var leftMargin = (Width / scale - Page.Width) / 2;

            g.ScaleTransform(scale, scale);
            g.TranslateTransform(leftMargin, topMargin);
            if (ShowGrid)
            {
                DrawGrid(g, scale);
            }
            if (ShowMarks)
            {
                DrawMarks(g);
            }
            DrawCell(g, scale);
            g.Restore(state);

            //DrawHist(g);
        }
        #endregion

        #region Begin/End
        public CustomPictureBox()
        {
            InitializeComponent();
            MouseMove += (sender, e) =>
            {
                MousePt = e.Location;
                Invalidate();
            };
            MouseLeave += (sender, e) =>
            {
                MousePt = null;
                Invalidate();
            };
        }
        #endregion

        #region Members
        public PageInfo Page { get; set; }
        public Point? MousePt { get; set; }
        public MarkInfo LastMark { get; set; }

        public bool ShowGrid { get; set; }
        public bool ShowMarks { get; set; }
        public bool ShowScore { get; set; }
        #endregion
    }
}
