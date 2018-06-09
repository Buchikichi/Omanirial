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
            var p = LastMark.Location;

            g.DrawRectangle(Pens.Red, new Rectangle(p.X - r, p.Y - r, w, w));
        }

        private void DrawMark(Graphics g, float scale)
        {
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in Page.MarkList)
            {
                if (mark.IsMarked)
                {
                    var pt = mark.Location;

                    g.DrawEllipse(Pens.Blue, new Rectangle(pt.X - r, pt.Y - r, w, w));
                }
            }
        }

        private void DrawGrid(Graphics g, float scale)
        {
            var r = pref.MarkRadius;
            var w = r * 2;

            using (var pen = new Pen(Brushes.LightGreen, .2f))
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
            //DrawGrid(g, scale);
            DrawMark(g, scale);
            DrawCell(g, scale);
            g.Restore(state);

            if (LastMark != null)
            {
                var x = 0;

                foreach (var b in LastMark.Hist)
                {
                    g.DrawLine(Pens.Blue, new Point(x, 0), new Point(x, b));
                    x += 4;
                    if (1 < x)
                    {
                        //return;
                    }
                }
            }
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
        public MarkInfo LastMark { get; set; }
        #endregion
    }
}
