using AxFiScnLib;
using Omanirial.data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Omanirial.scan
{
    class Scanner
    {
        #region メンバー
        private const int FILE_TYPE_JPEG = 3;
        private const int MAX_COUNT = 65535;

        private List<PageInfo> fileList = new List<PageInfo>();
        private AxFiScn fiScan = new AxFiScn();
        private int HWnd => ParentForm.Handle.ToInt32();
        #endregion

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
            fileList.Clear();
            Dict.Clear();
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
            fiScan.PixelType = 2; // RGB
            fiScan.Overwrite = 1;
            fiScan.PaperSupply = 2; // 2:ADF(Duplex)
            fiScan.Rotation = 1;
            fiScan.Resolution = 0; // 0:200dpi
            fiScan.ShowSourceUI = false;
        }

        private void SetupEvents()
        {
            fiScan.ScanToFile += (sender, e) =>
            {
                Debug.WriteLine($"ScanToFile:{e.scanCount}:{e.fileName}");
                fileList.Add(new PageInfo(e.fileName));
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

        #region Properties
        public Form ParentForm { get; set; }
        public string ImageDir { get; set; }
        public int Counter { get; set; }
        public Dictionary<string, PageInfo> Dict { get; } = new Dictionary<string, PageInfo>();
        #endregion
    }
}
