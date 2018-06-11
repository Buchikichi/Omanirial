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
            }
        }

        private void DrawMarks(Graphics g, float scale)
        {
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in Page.MarkList)
            {
                if (500 < mark.Score)
                {
                    var pt = mark.Location;

                    g.DrawEllipse(Pens.LimeGreen, new Rectangle(pt.X - r, pt.Y - r, w, w));
                }
            }
        }

        private void DrawGrid(Graphics g, float scale)
        {
            using (var pen = new Pen(Brushes.LightSkyBlue, .2f))
            {
                foreach (var pt in Page.TimingMarkList)
                {
                    g.DrawLine(pen, new Point(pt.X, 0), pt);
                }
                var y = Page.MarkAreaBottom;

                for (var ix = 0; ix < Page.MarkAreaRows; ix++)
                {
                    g.DrawLine(pen, new Point(0, y), new Point(Page.Width - 100, y));
                    y -= Page.MarkPitch;
                }
            }
            g.DrawEllipse(Pens.Red, new Rectangle(Page.Width / 2 - 1, Page.TimingMarkTop - 1, 2, 2));
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
                DrawMarks(g, scale);
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
        #endregion
    }
}
