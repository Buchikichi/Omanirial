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
        private List<MarkInfo> _markList;

        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public List<Point> TimingMarkList { get; set; } = new List<Point>();
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
        public List<MarkInfo> MarkList
        {
            get
            {
                if (_markList != null && 0 < _markList.Count)
                {
                    return _markList;
                }
                var y = MarkAreaBottom;

                _markList = new List<MarkInfo>();
                for (var ix = 0; ix < MarkAreaRows; ix++)
                {
                    foreach (var pt in TimingMarkList)
                    {
                        _markList.Add(new MarkInfo { Location = new Point(pt.X, y) });
                    }
                    y -= MarkPitch;
                }
                _markList.Sort((m1, m2) =>
                {
                    var p1 = m1.Location;
                    var p2 = m2.Location;
                    var v1 = p1.X + p1.Y * Width;
                    var v2 = p2.X + p2.Y * Height;

                    return v1 - v2;
                });
                return _markList;
            }
        }
        #endregion

        public PageInfo(string text)
        {
            Filename = text;
        }

        public static PageInfo Create(string filename)
        {
            var page = new PageInfo(filename);

            using (var img = new Mat(filename))
            {
                page.Width = img.Width;
                page.Height = img.Height;
                ImageUtils.FilterBW(img, page.MarkColorThreshold);
                page.TimingMarkList.AddRange(ImageUtils.DetectTimingMarks(img, out bool isUpsideDown));
                page.IsUpsideDown = isUpsideDown;
            }
            return page;
        }

        public override string ToString()
        {
            return Path.GetFileName(Filename);
        }
    }
}
