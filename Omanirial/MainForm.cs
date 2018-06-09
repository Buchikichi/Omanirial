using Omanirial.data;
using Omanirial.Properties;
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
        private Preference pref = Preference.Instance;
        private MarkRecognizer recognizer = new MarkRecognizer();

        private void LoadLayout()
        {
        }

        private bool CheckBaseDir()
        {
            var dir = BaseDirTextBox.Text;

            if (!Directory.Exists(dir))
            {
                Balloon.Show("入出力ディレクトリーを指定してください。", BaseDirTextBox, 0, -BaseDirTextBox.Height * 3);
                return false;
            }
            return true;
        }

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
            recognizer.Recognize(page);
            BasePictureBox.Page = page;
            BasePictureBox.Image?.Dispose();
            BasePictureBox.Image = Image.FromFile(page.Filename);
        }
        #endregion

        #region Event
        private void CreateButton_Click(object sender, System.EventArgs e)
        {
            if (!CheckBaseDir())
            {
                return;
            }
            var next = new EditingForm();

            Hide();
            next.ShowDialog(this);
            Show();
            Debug.Print("Dialog");
        }

        private void BaseDirTextBox_DoubleClick(object sender, System.EventArgs e)
        {
            if (!CheckBaseDir())
            {
                return;
            }
            Process.Start(BaseDirTextBox.Text);
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            Preference.Instance.Save();
        }
        #endregion

        #region Begin/End
        private void RefreshControls()
        {
            CheckBaseDir();
        }

        private void ResetControls()
        {
            Balloon.RemoveAll();
            RefreshControls();
        }

        private void Initialize()
        {
            Shown += (sender, e) => ResetControls();
            FormClosing += (sender, e) => Settings.Default.Save();
        }
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
    }
}
