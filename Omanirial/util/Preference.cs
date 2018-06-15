using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Omanirial.util
{
    public class Preference
    {
        private const string PREF_FILE = "preference.json";

        public static PreferenceData Instance = Load();

        protected Preference() { }

        private static PreferenceData Load()
        {
            var filename = Application.StartupPath + '\\' + PREF_FILE;
            var serializer = new DataContractJsonSerializer(typeof(PreferenceData));

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return (PreferenceData)serializer.ReadObject(stream);
            }
        }

        public void Save()
        {
            var filename = Application.StartupPath + '\\' + PREF_FILE;
            var serializer = new DataContractJsonSerializer(typeof(PreferenceData));

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                serializer.WriteObject(stream, this);
            }
        }
    }

    public class PreferenceData : Preference
    {
        public string LayoutDir { get; set; }
        public string TemporaryDir { get; set; }

        public int Dpi { get; set; } = 200;
        public int MarkAreaDefaultHeight { get; set; }
        public int MarkAreaDefaultRows { get; set; }
        public int MarkColorThreshold { get; set; }
        public int MarkRadius { get; set; } = 17;
        public int TimingMarkAreaHeight { get; set; }
        public int TimingMarkMinHeight { get; set; }
    }
}
