using Emgu.CV;
using Omanirial.util;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace Omanirial.data
{
    [DataContract]
    public class PageInfo
    {
        #region Attributes
        private int _timingMarkTop;

        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public List<Point> TimingMarkList { get; set; } = new List<Point>();
        [DataMember]
        public List<MarkInfo> MarkList { get; set; } = new List<MarkInfo>();
        [DataMember]
        public bool IsUpsideDown { get; set; }
        [DataMember]
        public int MarkMargin { get; set; } = 55;
        [DataMember]
        public int MarkPitch { get; set; } = 50;
        [DataMember]
        public int MarkAreaRows { get; set; } = 30;
        [DataMember]
        public int MarkColorThreshold { get; set; } = 150;//180;

        public int MarkAreaBottom => TimingMarkTop - MarkMargin;
        public int TimingMarkTop
        {
            get
            {
                if (_timingMarkTop == 0 && 0 < TimingMarkList.Count)
                {
                    var total = 0;

                    foreach (var pt in TimingMarkList)
                    {
                        total += pt.Y;
                    }
                    _timingMarkTop = total / TimingMarkList.Count;
                }
                return _timingMarkTop;
            }
        }
        #endregion

        #region Method
        public MarkInfo FindMark(Point pt)
        {
            MarkInfo result = null;
            var pref = Preference.Instance;
            var r = pref.MarkRadius;
            var w = r * 2;

            foreach (var mark in MarkList)
            {
                var rect = new Rectangle(mark.Location.X - r, mark.Location.Y - MarkPitch / 2, w, MarkPitch);

                if (rect.Contains(pt))
                {
                    result = mark;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region Begin/End
        public override string ToString() => Path.GetFileName(Filename);

        public void SetupMarkList()
        {
            var y = MarkAreaBottom;

            for (var ix = 0; ix < MarkAreaRows; ix++)
            {
                foreach (var pt in TimingMarkList)
                {
                    MarkList.Add(new MarkInfo { Location = new Point(pt.X, y) });
                }
                y -= MarkPitch;
            }
            MarkList.Sort((m1, m2) =>
            {
                var p1 = m1.Location;
                var p2 = m2.Location;
                var v1 = p1.X + p1.Y * Width;
                var v2 = p2.X + p2.Y * Height;

                return v1 - v2;
            });
        }

        public void DetectTimingMarks(string filename)
        {
            using (var img = new Mat(filename))
            {
                Width = img.Width;
                Height = img.Height;
                ImageUtils.FilterBW(img, MarkColorThreshold);
                TimingMarkList.AddRange(ImageUtils.DetectTimingMarks(img, out bool isUpsideDown));
                IsUpsideDown = isUpsideDown;
            }
            SetupMarkList();
            Filename = filename;
        }

        public PageInfo(string filename)
        {
            DetectTimingMarks(filename);
        }
        #endregion
    }
}
