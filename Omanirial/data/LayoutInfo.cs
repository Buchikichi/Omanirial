using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omanirial.data
{
    public class LayoutInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<PageInfo> PageList { get; } = new List<PageInfo>();
    }
}
