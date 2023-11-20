using System;
using System.Collections.Generic;
using System.Text;
using UnknownOS.Core;
using UnknownOS.Programs;
using Sys = Cosmos.System;

namespace UnknownOS
{
    public class Kernel : Sys.Kernel
    {
        public static Kernel Instance;
        public ProcessManager processManager;
        public SettingsManager settingsManager;

        protected override void BeforeRun()
        {
            Instance = this;
            processManager = new ProcessManager();
            settingsManager = new SettingsManager();
            Process.Instantiate(new StartUpProc());
        }

        protected override void Run()
        {
            processManager.Update();
        }
    }
}
