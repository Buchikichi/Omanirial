using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Omanirial.util
{
    public class ImageUtils
    {
        public static void FilterBW(Mat img, int t)
        {
            var lower = new ScalarArray(new MCvScalar(0, 0, 0));
            var upper = new ScalarArray(new MCvScalar(t, t, t));

            CvInvoke.InRange(img, lower, upper, img);
        }

        private static bool IsUpsideDown(Mat img, List<VectorOfPoint> list)
        {
            var pref = Preference.Instance;
            var numOfTop = 0;
            var numOfBottom = 0;
            var top = pref.TimingMarkAreaHeight;
            var bottom = img.Height - top;

            foreach (var vec in list)
            {
                var pt = vec[0];

                if (pt.Y <= top)
                {
                    numOfTop++;
                }
                if (bottom <= pt.Y)
                {
                    numOfBottom++;
                }
                Debug.Print($"pt.Y:{pt.Y}");
            }
            Debug.Print($"top:{numOfTop}/bottom:{numOfBottom}");
            return numOfBottom < numOfTop;
        }

        private static List<Point> RefillTimingMarks(List<VectorOfPoint> timingMarks, Mat img, bool isUpsideDown)
        {
            var list = new List<Point>();
            var width = img.Width;
            var height = img.Height;

            if (isUpsideDown)
            {
                foreach (var vec in timingMarks)
                {
                    var pt = vec[1];

                    list.Add(new Point(width - pt.X, height - pt.Y));
                }
            }
            else
            {
                foreach (var vec in timingMarks)
                {
                    list.Add(vec[0]);
                }
            }
            return list;
        }

        public static List<Point> DetectTimingMarks(Mat img, out bool isUpsideDown)
        {
            var timingMarks = new List<VectorOfPoint>();
            var pref = Preference.Instance;
            var top = pref.TimingMarkAreaHeight;
            var bottom = img.Height - top;

            var contours = new VectorOfVectorOfPoint();
            {
                CvInvoke.FindContours(img, contours, null, RetrType.List, ChainApproxMethod.LinkRuns);
                for (var ix = 0; ix < contours.Size; ix++)
                {
                    var vec = new VectorOfPoint();
                    var peri = CvInvoke.ArcLength(contours[ix], true);

                    CvInvoke.ApproxPolyDP(contours[ix], vec, peri * .1, true);
                    if (vec.Size <= 1 || 3 < vec.Size)
                    {
                        continue;
                    }
                    var beginPt = vec[0];
                    var nextPt = vec[1];
                    var height = Math.Abs(beginPt.Y - nextPt.Y);

                    if (height < pref.TimingMarkMinHeight)
                    {
                        continue;
                    }
                    //Debug.Print($"height:{height}");
                    var isOut = false;

                    for (var iy = 0; iy < vec.Size; iy++)
                    {
                        var pt = vec[iy];

                        if (top < pt.Y && pt.Y < bottom)
                        {
                            isOut = true;
                        }
                    }
                    if (isOut)
                    {
                        continue;
                    }
                    timingMarks.Add(vec);
                }
            }
            isUpsideDown = IsUpsideDown(img, timingMarks);
            return RefillTimingMarks(timingMarks, img, isUpsideDown);
        }
    }
}
