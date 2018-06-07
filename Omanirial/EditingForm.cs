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
        private PageInfo current;
        private Mat lastMat;
        #endregion

        #region PageListView
        private void LoadPageInfo(string filename)
        {
            var page = new PageInfo(filename);
            //var children = new TreeNode []{ new TreeNode("A"), new TreeNode("B") , new TreeNode("C") };

            using (var img = new Mat(filename))
            {
                ImageUtils.FilterBW(img);
                page.PointList.AddRange(ImageUtils.DetectTimingMarks(img));
                page.IsUpsideDown = ImageUtils.IsUpsideDown(img, page.PointList);
                if (page.IsUpsideDown)
                {
                    ImageUtils.UpsideDown(page.PointList, img.Width, img.Height);
                }
            }
            //page.Nodes.AddRange(children);
            PageListView.Nodes.Add(page);
        }

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
                LoadPageInfo(name);
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
            foreach (var vec in page.PointList)
            {
                var beginPt = vec[0];
                var endPt = vec[1];

                if (beginPt.Y < 90)
                {
                    continue;
                }
                //using (var cn = new VectorOfVectorOfPoint())
                //{
                //    cn.Push(vec);
                //    CvInvoke.DrawContours(img, cn, 0, new MCvScalar(0, 0, 200), 4);
                //}
                CvInvoke.Line(img, beginPt, new Point(beginPt.X, 0), new MCvScalar(200, 255, 200));
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
            if (current == parent)
            {
                return;
            }
            DrawImage(parent);
            ColumnsTextBox.Text = parent.PointList.Count.ToString();
            current = parent;
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
