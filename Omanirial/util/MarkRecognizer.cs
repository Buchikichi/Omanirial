using Emgu.CV;
using Emgu.CV.Util;
using Omanirial.data;
using System.Drawing;

namespace Omanirial.util
{
    public class MarkRecognizer
    {
        private PreferenceData pref = Preference.Instance;

        private int CalcScore(MarkInfo mark)
        {
            var max = (int)(mark.Hist.Length * .65);
            var total = 0;

            for (var ix = 0; ix < max; ix++)
            {
                total += mark.Hist[ix];
            }
            return total;
        }

        public void Recognize(PageInfo page)
        {
            var r = pref.MarkRadius;
            var w = r * 2;

            using (var img = new Mat(page.Filename))
            {
                CvInvoke.MedianBlur(img, img, 3);
                //ImageUtils.FilterBW(img, page.MarkColorThreshold);
                ImageUtils.RedFilter(img);
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
                        mark.Hist = hist.GetData();
                        mark.Score = CalcScore(mark);
                    }
                }
            }
        }
    }
}
