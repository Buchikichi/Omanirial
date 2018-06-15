using System.Windows.Forms;

namespace Omanirial.data
{
    class MarkBlockNode : TreeNode
    {
        private MarkBlockInfo _block;

        public MarkBlockNode(MarkBlockInfo block) : base(block.Number.ToString("000"))
        {
            _block = block;
        }
    }
}
