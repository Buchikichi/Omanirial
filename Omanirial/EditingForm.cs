using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Omanirial.data;
using Omanirial.util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class EditingForm : Form
    {
        #region Member
        private PageInfo currentPage;
        private Mat lastMat;
        #endregion

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
            var cnt = 0;

            ProgressBar.Value = 0;
            ProgressBar.Maximum = nameList.Length;
            foreach (var name in nameList)
            {
                ProgressBar.Value = ++cnt;
                if (!name.EndsWith(".jpg") && !name.EndsWith(".jpeg"))
                {
                    continue;
                }
                PageListView.Nodes.Add(PageInfo.Create(name));
            }
            Initialize();
            PageListView.ExpandAll();
        }

        private void DrawImage(PageInfo page)
        {
            var img = new Mat(page.Filename);

            if (page.IsUpsideDown)
            {
                CvInvoke.Flip(img, img, FlipType.Horizontal);
                CvInvoke.Flip(img, img, FlipType.Vertical);
            }
            foreach (var pt in page.PointList)
            {
                CvInvoke.Line(img, pt, new Point(pt.X, 0), new MCvScalar(200, 255, 200));
            }
            var y = page.MarkAreaBottom;

            for (var ix = 0; ix < page.MarkAreaRows; ix++)
            {
                CvInvoke.Line(img, new Point(0, y), new Point(img.Width, y), new MCvScalar(200, 255, 200));
                y -= 50;
            }
            lastMat?.Dispose();
            lastMat = img;
            OriginalPictureBox.Image?.Dispose();
            OriginalPictureBox.Image = img.Bitmap;
        }

        private void PageListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PageInfo parent = null;

            if (e.Node is PageInfo page)
            {
                parent = page;
            }
            else if (e.Node is TreeNode child)
            {
                parent = (PageInfo)child.Parent;
            }
            if (currentPage == parent)
            {
                return;
            }
            DrawImage(parent);
            ColumnsTextBox.Text = parent.PointList.Count.ToString();
            RowsUpDown.Value = parent.MarkAreaRows;
            currentPage = parent;
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
            ProgressBar.Value = 0;
        }

        public EditingForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
    }
}
