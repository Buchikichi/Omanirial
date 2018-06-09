using Emgu.CV;
using Omanirial.util;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Omanirial.data
{
    public class PageInfo : TreeNode
    {
        private int _timingMarkTop;
        private List<MarkInfo> _markList = new List<MarkInfo>();

        public string Filename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Point> TimingMarkList { get; } = new List<Point>();
        public bool IsUpsideDown { get; set; }
        public int MarkMargin { get; set; } = 55;
        public int MarkPitch { get; set; } = 50;
        public int MarkAreaRows { get; set; } = 30;
        public int MarkColorThreshold { get; set; } = 150;//180;
        public int MarkAreaBottom => TimingMarkTop - MarkMargin;

        public int TimingMarkTop
        {
            get
            {
                if (_timingMarkTop == 0)
                {
                    var total = 0;

                    foreach (var pt in TimingMarkList)
                    {
                        total += pt.Y;
                    }
                    _timingMarkTop = total / TimingMarkList.Count;
                }
                return _timingMarkTop;
            }
        }
        public List<MarkInfo> MarkList
        {
            get
            {
                if (0 < _markList.Count)
                {
                    return _markList;
                }
                var y = MarkAreaBottom;

                for (var ix = 0; ix < MarkAreaRows; ix++)
                {
                    foreach (var pt in TimingMarkList)
                    {
                        _markList.Add(new MarkInfo { Location = new Point(pt.X, y) });
                    }
                    y -= MarkPitch;
                }
                _markList.Sort((m1, m2) =>
                {
                    var p1 = m1.Location;
                    var p2 = m2.Location;
                    var v1 = p1.X + p1.Y * Width;
                    var v2 = p2.X + p2.Y * Height;

                    return v1 - v2;
                });
                return _markList;
            }
        }

        public PageInfo(string text) : base(Path.GetFileName(text))
        {
            Filename = text;
        }

        public static PageInfo Create(string filename)
        {
            var page = new PageInfo(filename);

            using (var img = new Mat(filename))
            {
                page.Width = img.Width;
                page.Height = img.Height;
                ImageUtils.FilterBW(img, page.MarkColorThreshold);
                page.TimingMarkList.AddRange(ImageUtils.DetectTimingMarks(img, out bool isUpsideDown));
                page.IsUpsideDown = isUpsideDown;
            }
            return page;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
