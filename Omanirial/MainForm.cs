using Omanirial.data;
using Omanirial.Properties;
using Omanirial.scan;
using Omanirial.util;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class MainForm : Form
    {
        private bool CheckBaseDir()
        {
            var dir = BaseDirTextBox.Text;

            RefreshControls();
            Balloon.RemoveAll();
            if (!Directory.Exists(dir))
            {
                Balloon.Show("入出力ディレクトリーを正しく指定してください。", BaseDirTextBox, 0, -BaseDirTextBox.Height * 3);
                return false;
            }
            return true;
        }

        #region LayoutInfo
        private void ShowEditingForm(LayoutInfo layout = null)
        {
            if (!CheckBaseDir())
            {
                return;
            }
            var next = new EditingForm { CurrentLayout = layout };

            Hide();
            next.ShowDialog(this);
            Show();
        }

        private void AddLayoutButton_Click(object sender, EventArgs e) => ShowEditingForm();

        private void EditLayoutButton_Click(object sender, EventArgs e) => ShowEditingForm((LayoutInfo)LayoutListBox.SelectedItem);
        #endregion

        #region ImageListBox
        private void AddPage(string filename)
        {
            var newPage = new PageInfo(filename);
            var layout = (LayoutInfo)LayoutListBox.SelectedItem;

            ImageListBox.Items.Add(newPage);
            if (layout == null)
            {
                return;
            }
            var srcPage = layout.PageList[0];

            foreach (var mark in newPage.MarkList)
            {
                var srcMark = srcPage.FindMark(mark.Location);

                if (srcMark != null)
                {
                    mark.Disabled = srcMark.Disabled;
                }
            }
            //if (srcPage.MarkList.Count != newPage.MarkList.Count)
            //{
            //    return;
            //}
            //for (var ix = 0; ix < srcPage.MarkList.Count; ix++)
            //{
            //    var src = srcPage.MarkList[ix];
            //    var dst = newPage.MarkList[ix];

            //    dst.Disabled = src.Disabled;
            //}
        }

        private void ImageListBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void ImageListBox_DragDrop(object sender, DragEventArgs e)
        {
            var nameList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var name in nameList)
            {
                if (!name.EndsWith(".jpg") && !name.EndsWith(".jpeg"))
                {
                    continue;
                }
                AddPage(name);
            }
        }

        private void ImageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var page = (PageInfo)ImageListBox.SelectedItem;

            if (page == null)
            {
                return;
            }
            var recognizer = new MarkRecognizer();

            recognizer.Recognize(page);
            BasePictureBox.Page = page;
        }

        private void ImageListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }
            var page = (PageInfo)ImageListBox.SelectedItem;

            if (page == null)
            {
                return;
            }
            ImageListBox.Items.Remove(page);
            RefreshControls();
        }
        #endregion

        #region Scan
        private void ScanButton_Click(object sender, EventArgs e)
        {
            var pref = Preference.Instance;
            var dir = BaseDirTextBox.Text + Path.DirectorySeparatorChar + pref.TemporaryDir;
            var scanner = new Scanner
            {
                ParentForm = this,
                ImageDir = dir,
                Counter = scanCount,
            };
            StatusMessageLabel.Text = "スキャンしています...";
            scanner.Start();
            StatusMessageLabel.Text = "画像を分析しています...";
            StatusBar.Refresh();
            foreach (var info in scanner.FileList)
            {
                AddPage(info.Filename);
            }
            scanner.End();
            scanCount = scanner.Counter;
            StatusMessageLabel.Text = string.Empty;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveControl is Button)
            {
                return;
            }
            if (e.KeyCode != Keys.Space && e.KeyCode != Keys.Enter)
            {
                return;
            }
            ScanButton.PerformClick();
        }
        #endregion

        #region BasePictureBox
        private void BasePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var msg = string.Empty;

            if (0 < BasePictureBox.Score)
            {
                var sc = BasePictureBox.Score.ToString("#,0");

                msg = $"score:{sc}";
            }
            StatusLabel.Text = msg;
        }
        #endregion

        #region Event
        private void ShowGridButton_Click(object sender, EventArgs e)
        {
            BasePictureBox.ShowGrid = ShowGridButton.Checked;
            BasePictureBox.Invalidate();
        }

        private void ShowScoreButton_Click(object sender, EventArgs e)
        {
            BasePictureBox.ShowScore = ShowScoreButton.Checked;
            BasePictureBox.Invalidate();
        }

        private void ShowAttributesButton_Click(object sender, EventArgs e)
        {
            BasePictureBox.ShowAttributes = ShowAttributesButton.Checked;
            BasePictureBox.Invalidate();
        }

        private void BaseDirTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (!CheckBaseDir())
            {
                return;
            }
            Process.Start(BaseDirTextBox.Text);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Preference.Instance.Save();
        }
        #endregion

        #region Begin/End
        private void RefreshControls()
        {
            var layout = LayoutListBox.SelectedItem;
            var canEdit = layout != null;
            var canSave = 0 < ImageListBox.Items.Count;
            var page = (PageInfo)ImageListBox.SelectedItem;

            BasePictureBox.Page = page;
            LayoutListBox.Enabled = canEdit;
            EditLayoutButton.Enabled = canEdit;
            ScanButton.Enabled = canEdit;
            SaveButton.Enabled = canSave;
        }

        //private void ResetControls()
        //{
        //    Balloon.RemoveAll();
        //    RefreshControls();
        //}

        private void LoadLayoutInfo()
        {
            LayoutListBox.Items.Clear();
            if (!CheckBaseDir())
            {
                return;
            }
            var manager = new LayoutManager();

            foreach (var layout in manager.ListLayout())
            {
                LayoutListBox.Items.Add(layout);
            }
            if (0 < LayoutListBox.Items.Count)
            {
                LayoutListBox.SelectedIndex = 0;
            }
            RefreshControls();
        }

        private void Initialize()
        {
            Shown += (sender, e) => LoadLayoutInfo();
            BaseDirTextBox.Validated += (sender, e) => LoadLayoutInfo();
            FormClosing += (sender, e) => Settings.Default.Save();
        }
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Attributes
        private int scanCount;
        #endregion
    }
}
