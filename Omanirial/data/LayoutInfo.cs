using System.Collections.Generic;

namespace Omanirial.data
{
    public class LayoutInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<PageInfo> PageList { get; set; } = new List<PageInfo>();

        public override string ToString()
        {
            return Name;
        }
    }
}
