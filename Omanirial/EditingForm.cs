using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Omanirial.data;
using Omanirial.util;
using System;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class EditingForm : Form
    {
        #region Member
        private PageInfo currentPage;
        private Mat lastMat;
        public LayoutInfo CurrentLayout { get; set; }
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
                var page = PageInfo.Create(name);

                PageListView.Nodes.Add(new PageNode(page));
                CurrentLayout.PageList.Add(page);
            }
            PageListView.ExpandAll();
            RefreshControls();
        }

        private void DrawImage(PageInfo page)
        {
            var img = new Mat(page.Filename);
            var threshold = page.MarkColorThreshold;

            if (page.IsUpsideDown)
            {
                CvInvoke.Flip(img, img, FlipType.Horizontal);
                CvInvoke.Flip(img, img, FlipType.Vertical);
            }
            ThresholdLabel.Text = threshold.ToString();
            ThresholdBar.Value = threshold;
            //CvInvoke.MedianBlur(img, img, 3);

            //ImageUtils.FilterBW(img, threshold);
            //ImageUtils.RedFilter(img, threshold);
            lastMat?.Dispose();
            lastMat = img;
            BasePictureBox.Page = page;
            BasePictureBox.Image?.Dispose();
            BasePictureBox.Image = img.Bitmap;
        }

        private void PageListView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PageNode parent = null;

            if (e.Node is PageNode pageNode)
            {
                parent = pageNode;
            }
            else if (e.Node is TreeNode child)
            {
                parent = (PageNode)child.Parent;
            }
            var page = parent.Page;
            if (currentPage == page)
            {
                return;
            }
            currentPage = page;
            DrawImage(page);
            ColumnsTextBox.Text = page.TimingMarkList.Count.ToString();
            RowsUpDown.Value = page.MarkAreaRows;
        }
        #endregion

        #region ToolBar
        private void ShowGridButton_CheckedChanged(object sender, EventArgs e)
        {
            BasePictureBox.ShowGrid = ShowGridButton.Checked;
            BasePictureBox.Invalidate();
        }
        #endregion
        private void ThresholdBar_Scroll(object sender, EventArgs e)
        {
            var val = ThresholdBar.Value;

            ThresholdLabel.Text = val.ToString();
            currentPage.MarkColorThreshold = val;
            DrawImage(currentPage);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var manager = new LayoutManager();

            manager.Save(CurrentLayout);
            Close();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Begin/End
        private void RefreshControls()
        {
            var canSave = !string.IsNullOrWhiteSpace(TitleTextBox.Text) && 0 < CurrentLayout.PageList.Count;

            SaveButton.Enabled = canSave;
            ProgressBar.Value = 0;
        }

        private void LoadLayout()
        {
            TitleTextBox.Text = CurrentLayout.Name;

            foreach (var page in CurrentLayout.PageList)
            {
                PageListView.Nodes.Add(new PageNode(page));
            }
        }

        private void Initialize()
        {
            Load += (sender, e) =>
            {
                if (CurrentLayout == null)
                {
                    CurrentLayout = new LayoutInfo { ID = Guid.NewGuid().ToString() };
                }
                LoadLayout();
            };
            TitleTextBox.Validating += (sender, e) =>
            {
                CurrentLayout.Name = TitleTextBox.Text;
                RefreshControls();
            };
        }

        public EditingForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
    }
}
