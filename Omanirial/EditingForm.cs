using Omanirial.data;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class EditingForm : Form
    {
        #region PageListView
        private void PageListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void PageListView_DragDrop(object sender, DragEventArgs e)
        {
            var nameList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var name in nameList)
            {
                if (!name.EndsWith(".jpg") && !name.EndsWith(".jpeg"))
                {
                    continue;
                }
                var node = new PageInfo(name);

                PageListView.Nodes.Add(node);
                node.Nodes.Add(new TreeNode("A"));
                node.Nodes.Add(new TreeNode("B"));
                node.Nodes.Add(new TreeNode("C"));
            }
            PageListView.ExpandAll();
        }

        private void PageListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;

            if (node is PageInfo page)
            {
                Debug.Print($"AfterSelect:{page.Filename}");
            }
        }
        #endregion

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Begin/End
        private void Initialize()
        {
        }

        public EditingForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
    }
}
