using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Omanirial.data
{
    public class PageInfo : TreeNode
    {
        private int _timingMarkTop;

        public string Filename { get; set; }
        public List<Point> PointList { get; } = new List<Point>();
        public bool IsUpsideDown { get; set; }
        public int MarkMargin { get; set; } = 55;
        public int MarkAreaRows { get; set; } = 30;

        public int MarkAreaBottom
        {
            get
            {
                return TimingMarkTop - MarkMargin;
            }
        }

        public int TimingMarkTop
        {
            get
            {
                if (_timingMarkTop == 0)
                {
                    var total = 0;

                    foreach (var pt in PointList)
                    {
                        total += pt.Y;
                    }
                    _timingMarkTop = total / PointList.Count;
                }
                return _timingMarkTop;
            }
        }

        public PageInfo(string text) : base(Path.GetFileName(text))
        {
            Filename = text;
        }
    }
}
