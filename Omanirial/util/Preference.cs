using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace Omanirial.util
{
    public class Preference
    {
        private const string PREF_FILE = "preference.json";

        public static Preference Instance = Load();

        protected Preference() { }

        private static Preference Load()
        {
            var filename = Application.StartupPath + '\\' + PREF_FILE;
            var serializer = new DataContractJsonSerializer(typeof(PreferenceData));

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return (Preference)serializer.ReadObject(stream);
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
    }
}
