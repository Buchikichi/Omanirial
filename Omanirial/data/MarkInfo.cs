using System.Drawing;

namespace Omanirial.data
{
    public class MarkInfo
    {
        public Point Location { get; set; }
        public bool Disabled { get; set; }
        public byte[] Hist { get; set; }
        public int Score { get; set; }
    }
}
