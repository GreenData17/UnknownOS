using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownOS.Core
{
    public class SettingsManager
    {
        public Dictionary<string, string> Settings = new Dictionary<string, string>();

        public SettingsManager()
        {
            Settings.Add("global.system.name", "UnknownOS");
            Settings.Add("global.system.version", "v0.1");
            Settings.Add("global.system.state", "Alpha");

            Settings.Add("global.filesystem.root", "0:\\");
        }

        public string GetSetting(string key)
        {
            return Settings[key];
        }

        public void AddSetting(string key, string value)
        {
            Settings.Add(key, value);
        }

        public void UpdateSetting(string key, string value)
        {
            Settings[key] = value;
        }

        private void LoadSettingsFile()
        {
            // TODO: load on start up files in the /etc folder
        }
    }
}
