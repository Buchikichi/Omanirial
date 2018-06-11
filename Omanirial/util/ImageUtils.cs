using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Omanirial.util
{
    public class ImageUtils
    {
        public static void RedFilter(Mat img, int redThreshold = 0xe0)
        {
            using (var mask = new Mat())
            {
                // BGR
                var lower = new ScalarArray(new MCvScalar(0x90, 0xa0, redThreshold));
                var upper = new ScalarArray(new MCvScalar(0xff, 0xff, 0xff));

                CvInvoke.InRange(img, lower, upper, mask);
                //CvInvoke.Imshow("mask", mask);
                CvInvoke.BitwiseNot(img, img);
                CvInvoke.BitwiseNot(mask, mask);
                CvInvoke.CvtColor(mask, mask, ColorConversion.Gray2Bgr);
                CvInvoke.BitwiseAnd(img, mask, img);
                CvInvoke.BitwiseNot(img, img);
            }
        }

        public static void FilterBW(Mat img, int t)
        {
            var lower = new ScalarArray(new MCvScalar(0, 0, 0));
            var upper = new ScalarArray(new MCvScalar(t, t, t));

            CvInvoke.InRange(img, lower, upper, img);
        }

        private enum TimingMarkType { Out = 0, Top = 1, Bottom = 2 }

        public static List<Point> DetectTimingMarks(Mat img, out bool isUpsideDown)
        {
            var topMarks = new List<Point>();
            var bottomMarks = new List<Point>();
            var pref = Preference.Instance;
            var top = pref.TimingMarkAreaHeight;
            var bottom = img.Height - top;
            var left = top;
            var right = img.Width - top;
            var width = img.Width;
            var height = img.Height;

            var contours = new VectorOfVectorOfPoint();
            {
                CvInvoke.FindContours(img, contours, null, RetrType.List, ChainApproxMethod.LinkRuns);
                for (var ix = 0; ix < contours.Size; ix++)
                {
                    var vec = new VectorOfPoint();
                    var peri = CvInvoke.ArcLength(contours[ix], true);

                    CvInvoke.ApproxPolyDP(contours[ix], vec, peri * .1, true);
                    if (vec.Size <= 1 || 2 < vec.Size)
                    {
                        continue;
                    }
                    var beginPt = vec[0];
                    var nextPt = vec[1];
                    var markHeight = Math.Abs(beginPt.Y - nextPt.Y);

                    if (markHeight < pref.TimingMarkMinHeight)
                    {
                        continue;
                    }
                    //Debug.Print($"height:{height}");
                    var type = TimingMarkType.Out;

                    for (var iy = 0; iy < vec.Size; iy++)
                    {
                        var pt = vec[iy];

                        if (pt.X < left || right < pt.X)
                        {
                            break;
                        }
                        if (pt.Y <= top)
                        {
                            type = TimingMarkType.Top;
                        }
                        else if (bottom <= pt.Y)
                        {
                            type = TimingMarkType.Bottom;
                        }
                    }
                    if (type == TimingMarkType.Out)
                    {
                        continue;
                    }
                    else if (type == TimingMarkType.Top)
                    {
                        var src = vec[1];
                        topMarks.Add(new Point(width - src.X, height - src.Y));
                    }
                    else if (type == TimingMarkType.Bottom)
                    {
                        bottomMarks.Add(vec[0]);
                    }
                }
            }
            isUpsideDown = bottomMarks.Count <  topMarks.Count;
            if (isUpsideDown)
            {
                return topMarks;
            }
            return bottomMarks;
        }
    }
}
