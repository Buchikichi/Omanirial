using System.Drawing;

namespace Omanirial.data
{
    public class MarkInfo
    {
        public Point Location { get; set; }
        public byte[] Hist { get; set; }
        public bool IsMarked { get; set; }
    }
}
