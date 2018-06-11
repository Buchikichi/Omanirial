using Omanirial.data;
using Omanirial.Properties;
using Omanirial.scan;
using Omanirial.util;
using System;
using System.Diagnostics;
using System.Drawing;
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
        private void ShowEditingForm()
        {
            if (!CheckBaseDir())
            {
                return;
            }
            var layout = (LayoutInfo)LayoutListBox.SelectedItem;
            var next = new EditingForm { CurrentLayout = layout };

            Hide();
            next.ShowDialog(this);
            Show();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            ShowEditingForm();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            ShowEditingForm();
        }
        #endregion

        #region ImageListBox
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
                ImageListBox.Items.Add(PageInfo.Create(name));
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
            BasePictureBox.Image?.Dispose();
            BasePictureBox.Image = Image.FromFile(page.Filename);
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
            scanner.Start();
            scanner.End();
            scanCount = scanner.Counter;
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

        #region Event
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

            EditButton.Enabled = canEdit;
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
