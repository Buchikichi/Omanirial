using System.IO;
using System.Windows.Forms;

namespace Omanirial.data
{
    public class PageNode : TreeNode
    {
        private PageInfo _page;
        public PageInfo Page => _page;

        public PageNode(PageInfo page) : base(Path.GetFileName(page.Filename))
        {
            _page = page;
        }
    }
}
