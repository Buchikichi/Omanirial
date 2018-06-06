using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Omanirial.util;
using System.Diagnostics;
using System.Windows.Forms;

namespace Omanirial
{
    public partial class MainForm : Form
    {
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
            var list = ImageUtils.DetectTimingMarks(mask);
            var ud = ImageUtils.IsUpsideDown(mask, list);

            Debug.Print($"IsUpsideDown:{ud}");
            foreach (var vec in list)
            {
                var cn = new VectorOfVectorOfPoint();

                cn.Push(vec);
                CvInvoke.DrawContours(img, cn, 0, new MCvScalar(0, 0, 200), 4);
            }
            BasePictureBox.Image?.Dispose();
            BasePictureBox.Image = img.Bitmap;
        }

        #region Event
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
        #endregion

        #region Begin/End
        private void Initialize()
        {
            BasePictureBox.AllowDrop = true;
        }
        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
    }
}
