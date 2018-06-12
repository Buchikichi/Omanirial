using Omanirial.Properties;
using Omanirial.util;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Omanirial.data
{
    public class LayoutManager
    {
        #region Attributes
        private PreferenceData pref = Preference.Instance;
        private string LayoutDir
        {
            get
            {
                return Settings.Default.BaseDir + Path.DirectorySeparatorChar + pref.LayoutDir;
            }
        }
        #endregion

        private LayoutInfo LoadLayout(string filename)
        {
            var serializer = new DataContractJsonSerializer(typeof(LayoutInfo));

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return (LayoutInfo)serializer.ReadObject(stream);
            }
        }

        public List<LayoutInfo> ListLayout()
        {
            var list = new List<LayoutInfo>();

            if (!Directory.Exists(LayoutDir))
            {
                return list;
            }
            foreach (var dir in Directory.GetDirectories(LayoutDir))
            {
                var file = Directory.GetFiles(dir, "*.json")[0];

                Debug.Print($"dir[{dir}]");
                list.Add(LoadLayout(file));
            }
            return list;
        }

        public void Save(LayoutInfo layout)
        {
            var baseDir = LayoutDir + Path.DirectorySeparatorChar + layout.ID;
            var filename = baseDir + Path.DirectorySeparatorChar + $"{layout.ID}.json";
            var serializer = new DataContractJsonSerializer(typeof(LayoutInfo));

            Directory.CreateDirectory(baseDir);
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                serializer.WriteObject(stream, layout);
            }
        }
    }
}
