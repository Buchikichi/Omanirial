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
            var r = pref.MarkRadius;
            var w = r * 2;
            var mx = (int)(MousePt.Value.X / scale - leftMargin);
            var my = (int)(MousePt.Value.Y / scale - topMargin);
            var dx = Width;
            var dy = Height;
            var y = Page.MarkAreaBottom;

            foreach (var pt in Page.PointList)
            {
                var diff = pt.X - mx;

                if (Math.Abs(diff) < Math.Abs(dx))
                {
                    dx = diff;
                }
            }
            mx += dx;
            for (var ix = 0; ix < Page.MarkAreaRows; ix++)
            {
                var diff = y - my;

                if (Math.Abs(diff) < Math.Abs(dy))
                {
                    dy = diff;
                }
                y -= Page.MarkPitch;
            }
            my += dy;
            g.DrawEllipse(Pens.Red, new RectangleF(mx - r, my - r, w, w));
        }

        private void DrawGrid(Graphics g, float scale)
        {
            using (var pen = new Pen(Brushes.LightGreen, .2f))
            {
                foreach (var pt in Page.PointList)
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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Page == null)
            {
                return;
            }
            var g = e.Graphics;
            var scale = CalcScale(g);
            var topMargin = (Height / scale - Page.Height) / 2;
            var leftMargin = (Width / scale - Page.Width) / 2;

            g.ScaleTransform(scale, scale);
            g.TranslateTransform(leftMargin, topMargin);
            DrawGrid(g, scale);
            DrawCell(g, scale);
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
        private PreferenceData pref = Preference.Instance;

        public PageInfo Page { get; set; }
        public Point? MousePt { get; set; }
        #endregion
    }
}
