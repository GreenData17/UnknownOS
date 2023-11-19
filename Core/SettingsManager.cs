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

        private void LoadSettingsFile()
        {
            // TODO: load on start up files in the /etc folder
        }
    }
}
