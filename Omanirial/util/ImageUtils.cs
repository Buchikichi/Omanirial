using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Omanirial.util
{
    public class ImageUtils
    {
        private const int Margin = 90;
        private const int TimingMarkMinHeight = 30;

        public static List<VectorOfPoint> DetectTimingMarks(Mat img)
        {
            var list = new List<VectorOfPoint>();
            var top = Margin;
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

        public static bool IsUpsideDown(Mat img, List<VectorOfPoint> list)
        {
            var numOfTop = 0;
            var numOfBottom = 0;
            var top = Margin;
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
