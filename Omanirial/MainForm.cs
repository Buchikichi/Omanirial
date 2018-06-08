using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Omanirial.Properties;
using Omanirial.util;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class MainForm : Form
    {
        private Preference pref = Preference.Instance;

        private void LoadImage(string filename)
        {
            var img = new Mat(filename);
            var height = img.Height;
            var top = 90;
            var bottom = height - top;
            var mask = new Mat();
            var lower = new ScalarArray(new MCvScalar(0, 0, 0));
            var upper = new ScalarArray(new MCvScalar(180, 180, 180));

            CvInvoke.InRange(img, lower, upper, mask);
            //CvInvoke.BitwiseNot(mask, mask);
            var list = ImageUtils.DetectTimingMarks(mask, out bool isUpsideDown);

            Debug.Print($"IsUpsideDown:{isUpsideDown}");
            foreach (var vec in list)
            {
                //var cn = new VectorOfVectorOfPoint();

                //cn.Push(vec);
                //CvInvoke.DrawContours(img, cn, 0, new MCvScalar(0, 0, 200), 4);
            }
            BasePictureBox.Image?.Dispose();
            BasePictureBox.Image = img.Bitmap;
        }

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

        private void BasePictureBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void BasePictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var nameList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var name in nameList)
            {
                if (!name.EndsWith(".jpg") && !name.EndsWith(".jpeg"))
                {
                    continue;
                }
                LoadImage(name);
                break;
            }
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
            BasePictureBox.AllowDrop = true;
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
