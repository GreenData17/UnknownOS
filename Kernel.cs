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
        public SettingsManager settingsManager;

        private readonly List<Process> _processes = new List<Process>();

        public void AddProcess(Process process) => _processes.Add(process);
        public void RemoveProcess(Process process) => _processes.Remove(process);

        protected override void BeforeRun()
        {
            Instance = this;
            settingsManager = new SettingsManager();
            Process.Instantiate(new StartUpProc());
        }

        protected override void Run()
        {
            for (int i = 0; i < 4; i++)
            {
                foreach (Process process in _processes)
                {
                    if (process.GetPriorityLevel() == (Process.PriorityLevel)i)
                    {
                        process.Update();
                    }
                }
            }
        }
    }
}
