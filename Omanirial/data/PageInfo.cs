using System.IO;
using System.Windows.Forms;

namespace Omanirial.data
{
    public class PageInfo : TreeNode
    {
        public string Filename { get; set; }

        public PageInfo(string text) : base(Path.GetFileName(text))
        {
            Filename = text;
        }
    }
}
