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
        private const int TimingMarkMinHeight = 30;

        public static void FilterBW(Mat img)
        {
            var lower = new ScalarArray(new MCvScalar(0, 0, 0));
            var upper = new ScalarArray(new MCvScalar(180, 180, 180));

            CvInvoke.InRange(img, lower, upper, img);
        }

        public static List<VectorOfPoint> DetectTimingMarks(Mat img)
        {
            var list = new List<VectorOfPoint>();
            var pref = Preference.Instance;
            var top = pref.TimingMarkHeight;
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

                    if (height < TimingMarkMinHeight)
                    {
                        continue;
                    }
                    Debug.Print($"height:{height}");
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
                    list.Add(vec);
                }
            }
            return list;
        }

        public static void UpsideDown(List<VectorOfPoint> list, int width, int height)
        {
            foreach (var vec in list)
            {
                var beginPt = vec[0];
                var endPt = vec[1];

                vec.Clear();
                vec.Push(new Point[] { new Point(width - endPt.X, height - endPt.Y), new Point(width - beginPt.X, height - beginPt.Y) });
            }
        }

        public static bool IsUpsideDown(Mat img, List<VectorOfPoint> list)
        {
            var pref = Preference.Instance;
            var numOfTop = 0;
            var numOfBottom = 0;
            var top = pref.TimingMarkHeight;
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
            }
            Debug.Print($"top:{numOfTop}/bottom:{numOfBottom}");
            return numOfBottom < numOfTop;
        }
    }
}
