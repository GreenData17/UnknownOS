using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownOS.Core;

namespace UnknownOS.Programs.commands
{
    public class ls : Process
    {
        public ls():base("ls", PriorityLevel.High){}

        public override void Start()
        {
            string path = Kernel.Instance.settingsManager.GetSetting("global.console.session1.path");

            // directories
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                AddOutput(directory);
            }

            // files
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                AddOutput(file);
            }

            Destroy();
        }
    }
}
