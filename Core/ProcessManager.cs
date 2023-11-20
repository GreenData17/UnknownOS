using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownOS.Core
{
    public class ProcessManager
    {
        private List<Process> _processes = new List<Process>();
        private List<Process> _processesWaitForInit = new List<Process>();
        private List<Process> _processesWaitForDeletion = new List<Process>();

        public void AddProcess(Process process) => _processesWaitForInit.Add(process);
        public void RemoveProcess(Process process) => _processesWaitForDeletion.Add(process);

        public void Update()
        {
            if (_processesWaitForDeletion.Count > 0)
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
