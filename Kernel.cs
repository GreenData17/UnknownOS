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

        private List<Process> _processes = new List<Process>();
        private List<Process> _processesWaitForInit = new List<Process>();
        private List<Process> _processesWaitForDeletion = new List<Process>();

        public void AddProcess(Process process) => _processesWaitForInit.Add(process);
        public void RemoveProcess(Process process) => _processesWaitForDeletion.Add(process);

        protected override void BeforeRun()
        {
            Instance = this;
            settingsManager = new SettingsManager();
            Process.Instantiate(new StartUpProc());
        }

        protected override void Run()
        {
            if(_processesWaitForDeletion.Count > 0)
            {
                foreach (Process process in _processesWaitForDeletion)
                {
                    _processes.Remove(process);
                }
                _processesWaitForDeletion.Clear();
            }

            //   \/----- This fucking shit is needed.
            if (_processesWaitForInit.Count > 0)
            {
                foreach (Process process in _processesWaitForInit)
                {
                    _processes.Add(process);
                }
                _processesWaitForInit.Clear();
            }

            for (int i = 0; i < 4; i++)
            {
                // Without the shit above this list gets changed during runtime and fucks everything up.
                foreach (Process process in _processes)
                {
                    if (process.GetPriorityLevel() == (Process.PriorityLevel)i)
                    {
                        process.TriggerUpdate();
                    }
                }
            }
        }
    }
}
