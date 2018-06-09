using Emgu.CV;
using Emgu.CV.Util;
using Omanirial.data;
using System.Drawing;

namespace Omanirial.util
{
    public class MarkRecognizer
    {
        private PreferenceData pref = Preference.Instance;

        public void Recognize(PageInfo page)
        {
            var r = pref.MarkRadius;
            var w = r * 2;

            using (var img = new Mat(page.Filename))
            {
                ImageUtils.FilterBW(img, page.MarkColorThreshold);
                foreach (var mark in page.MarkList)
                {
                    var pt = mark.Location;

                    using (var mat = new Mat(img, new Rectangle(pt.X - r, pt.Y - r, w, w)))
                    using (var hist = new Mat())
                    {
                        var histSize = new int[] { 256 };
                        float[] ranges = { 0f, 256f };

                        using (var vm = new VectorOfMat())
                        {
                            vm.Push(mat);
                            CvInvoke.CalcHist(vm, new int[] { 0 }, null, hist, histSize, ranges, false);
                        }
                        mark.Hist = hist.GetData(0);
                        mark.IsMarked = 10 < mark.Hist[1];
                        //Debug.Print($"Rows:{hist.Rows}/Cols:{hist.Cols}");
                    }
                }
            }
        }
    }
}
