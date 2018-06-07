using Emgu.CV.Util;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Omanirial.data
{
    public class PageInfo : TreeNode
    {
        public string Filename { get; set; }
        public List<VectorOfPoint> PointList { get; } = new List<VectorOfPoint>();
        public bool IsUpsideDown { get; set; }

        public PageInfo(string text) : base(Path.GetFileName(text))
        {
            Filename = text;
        }
    }
}
