using Omanirial.data;
using Omanirial.util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Omanirial.behavior
{
    public partial class CustomPictureBox : PictureBox
    {
        #region MarkAttribute
        private void PutMaskAttribute(MarkInfo mark)
        {
            if (lastMark == null || lastMark == mark)
            {
                mark.Disabled = drawBegan.Value;
                return;
            }
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var px = (float)lastPt.Value.X;
            var py = (float)lastPt.Value.Y;
            var dx = mousePt.Value.X - px;
            var dy = mousePt.Value.Y - py;
            var sx = dx / r;
            var sy = dy / r;

            while (true)
            {
                px += sx;
                py += sy;
                var mk = FindMark(new Point((int)px, (int)py));

                if (mk == lastMark)
                {
                    continue;
                }
                if (mk == null)
                {
                    break;
                }
                mk.Disabled = drawBegan.Value;
                if (mk == mark)
                {
                    break;
                }
            }
        }

        private void PutMarkAttribute(MarkInfo mark)
        {
            if (drawBegan == null || mark == null || mark == lastMark)
            {
                return;
            }
            if (PutMask)
            {
                PutMaskAttribute(mark);
            }
            lastPt = mousePt;
            lastMark = mark;
        }
        #endregion

        #region Paint
        private void DrawHist(Graphics g, MarkInfo mark)
        {
            if (mark == null || mark.Hist == null)
            {
                return;
            }
            var x = 0;
            var max = (int)(mark.Hist.Length * .65);

            foreach (var b in mark.Hist)
            {
                if (x < 256)
                {
                    g.DrawLine(Pens.Red, new Point(x, 0), new Point(x, b));
                }
                else if (x < max)
                {
                    g.DrawLine(Pens.Orange, new Point(x, 0), new Point(x, b));
                }
                else
                {
                    g.DrawLine(Pens.Gray, new Point(x, 0), new Point(x, b));
                }
                x += 1;
            }
        }

        private void DrawCell(Graphics g, MarkInfo mark)
        {
            if (mark == null)
            {
                return;
            }
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;
            var p = mark.Location;

            using (var brush = new SolidBrush(Color.FromArgb(0x20, Color.Green)))
            {
                g.FillRectangle(brush, new Rectangle(p.X - r, p.Y - r, w, w));
            }
            g.DrawRectangle(Pens.Green, new Rectangle(p.X - r, p.Y - r, w, w));
        }

        private void DrawAttributes(Graphics g)
        {
            if (!ShowAttributes)
            {
                return;
            }
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in Page.MarkList)
            {
                var pt = mark.Location;

                if (mark.Disabled)
                {
                    using (var brush = new SolidBrush(Color.FromArgb(0x30, Color.Gray)))
                    {
                        g.FillRectangle(brush, new Rectangle(pt.X - r, pt.Y - r, w, w));
                    }
                }
            }
        }

        private void DrawScore(Graphics g, MarkInfo mark, Brush brush)
        {
            if (mark == null || mark.Score == 0)
            {
                return;
            }
            var pref = Preference.Instance;
            var pt = mark.Location;
            var r = pref.MarkRadius;

            using (var font = new Font(FontFamily.GenericMonospace, 14))
            {
                g.DrawString(mark.Score.ToString(), font, brush, new PointF(pt.X - r, pt.Y - r));
            }
        }

        private void DrawMarks(Graphics g)
        {
            if (!ShowMarks)
            {
                return;
            }
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in Page.MarkList)
            {
                if (mark.Disabled)
                {
                    continue;
                }
                var pt = mark.Location;

                if (MIN_SCORE < mark.Score)
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

        private MarkInfo FindMark(Point? pt)
        {
            if (!pt.HasValue)
            {
                return null;
            }
            var scale = CalcScale();
            var topMargin = (Height / scale - Page.Height) / 2;
            var leftMargin = (Width / scale - Page.Width) / 2;
            var mx = (int)(pt.Value.X / scale - leftMargin);
            var my = (int)(pt.Value.Y / scale - topMargin);

            return Page.FindMark(new Point(mx, my));
        }

        private float CalcScale()
        {
            var scaleH = (float)Width / Page.Width;
            var scaleV = (float)Height / Page.Height;

            return Math.Min(scaleH, scaleV);
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
            var scale = CalcScale();
            var topMargin = (Height / scale - Page.Height) / 2;
            var leftMargin = (Width / scale - Page.Width) / 2;
            var mark = FindMark(mousePt);

            PutMarkAttribute(mark);
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(leftMargin, topMargin);
            if (ShowGrid)
            {
                DrawGrid(g, scale);
            }
            DrawAttributes(g);
            DrawMarks(g);
            DrawCell(g, mark);
            DrawScore(g, mark, Brushes.SkyBlue);
            g.Restore(state);

            if (ShowScore)
            {
                DrawHist(g, mark);
            }
            Score = mark == null ? 0 : mark.Score;
        }
        #endregion

        #region Begin/End
        private void MouseEnd()
        {
            mousePt = null;
            lastPt = null;
            lastMark = null;
            drawBegan = null;
            Invalidate();
        }

        private void InitializeMouse()
        {
            MouseDown += (sender, e) =>
            {
                mousePt = e.Location;
                var mark = FindMark(mousePt);

                if (mark != null)
                {
                    drawBegan = !mark.Disabled;
                }
                Invalidate();
            };
            MouseMove += (sender, e) =>
            {
                mousePt = e.Location;
                Invalidate();
            };
            MouseUp += (sender, e) => MouseEnd();
            MouseLeave += (sender, e) => MouseEnd();
        }

        public CustomPictureBox()
        {
            InitializeComponent();
            InitializeMouse();
        }
        #endregion

        #region Members
        private const int MIN_SCORE = 3000;

        private bool? drawBegan;
        private PageInfo _page;
        private MarkInfo lastMark;
        private Point? mousePt;
        private Point? lastPt;

        public PageInfo Page
        {
            internal get => _page;
            set
            {
                if (_page == value)
                {
                    return;
                }
                _page = value;
                Image?.Dispose();
                if (_page == null)
                {
                    Image = null;
                }
                else
                {
                    Image = Image.FromFile(_page.Filename);
                }
            }
        }

        public int Score { get; internal set; }

        public bool PutMask { get; set; }

        public bool ShowGrid { get; set; }
        public bool ShowMarks { get; set; }
        public bool ShowScore { get; set; }
        public bool ShowAttributes { get; set; }
        #endregion
    }
}
