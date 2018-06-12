#define SCAN
#if SCAN
using AxFiScnLib;
#endif
using Omanirial.data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System;

namespace Omanirial.scan
{
    class Scanner
    {
        #region メソッド
        /// <summary>終了処理.</summary>
        private void Conclude()
        {
        }

        public void Start()
        {
            var filename = $"{ImageDir}\\im#######";

            Directory.CreateDirectory(ImageDir);
            if (!ParentForm.Controls.Contains(fiScan))
            {
                ParentForm.Controls.Add(fiScan);
            }
            fiScan.FileName = filename;
            fiScan.FileCounter = Counter;
            fiScan.EndorserCounter = Counter;
            fiScan.OpenScanner(HWnd);
            SetupOptions();
            Debug.WriteLine("StartScan");
            fiScan.StartScan(HWnd);
            // スキャンが終わるまで処理が進まない
            Conclude();
            fiScan.CloseScanner(HWnd);
            Debug.WriteLine("CloseScanner");
        }

        public void End()
        {
            FileList.Clear();
        }
        #endregion

        #region Initialize
        private void SetupOptions()
        {
            fiScan.AutoBorderDetection = true;
            fiScan.Binding = 1; // 両面とじ方向
            fiScan.BlankPageSkip = 0;
            fiScan.Deskew = 0; // 傾き補正 0:Edge
            fiScan.FileType = FILE_TYPE_JPEG;
            fiScan.Overwrite = 1;
            fiScan.PaperSupply = 2; // 2:ADF(Duplex)
            fiScan.PixelType = 2; // RGB
            fiScan.Resolution = 0; // 0:200dpi
            fiScan.Rotation = 1;
            fiScan.ShowSourceUI = false;
            fiScan.SkipWhitePage = 10;
        }

        private void SetupEvents()
        {
            fiScan.ScanToFile += (sender, e) =>
            {
                Debug.WriteLine($"ScanToFile:{e.scanCount}:{e.fileName}");
                FileList.Add(new PageInfo(e.fileName));
                Counter++;
                if (MAX_COUNT < Counter)
                {
                    Counter = 1;
                }
            };
        }

        public Scanner()
        {
            SetupEvents();
        }
        #endregion

        #region Attributes
        private const int FILE_TYPE_JPEG = 3;
        private const int MAX_COUNT = 65535;

        private AxFiScn fiScan = new AxFiScn();
        private int HWnd => ParentForm.Handle.ToInt32();

        public Form ParentForm { get; set; }
        public string ImageDir { get; set; }
        public int Counter { get; set; }
        public List<PageInfo> FileList { get; } = new List<PageInfo>();
        #endregion
    }

    #region AxFiScn stub
#if !SCAN
    class AxFiScn : Control
    {
        public string FileName { get; internal set; }
        public int FileCounter { get; internal set; }
        public int EndorserCounter { get; internal set; }
        public Action<object, DummyArgs> ScanToFile { get; internal set; }
        public bool AutoBorderDetection { get; internal set; }
        public int Binding { get; internal set; }
        public int BlankPageSkip { get; internal set; }
        public int Deskew { get; internal set; }
        public int FileType { get; internal set; }
        public int Overwrite { get; internal set; }
        public int PaperSupply { get; internal set; }
        public int PixelType { get; internal set; }
        public int Resolution { get; internal set; }
        public int Rotation { get; internal set; }
        public bool ShowSourceUI { get; internal set; }
        public int SkipWhitePage { get; internal set; }

        internal void OpenScanner(int hWnd) { }
        internal void StartScan(int hWnd) { }
        internal void CloseScanner(int hWnd) { }
    }
    class DummyArgs
    {
        internal string fileName;
        internal int scanCount;
    }
#endif
    #endregion
}
