using System.Drawing;
using System.Runtime.Serialization;

namespace Omanirial.data
{
    [DataContract]
    public class MarkInfo
    {
        [DataMember]
        public Point Location { get; set; }
        [DataMember]
        public bool Disabled { get; set; }
        public byte[] Hist { get; set; }
        public int Score { get; set; }
    }
}
