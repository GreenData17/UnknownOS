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
        public TextModeConsole console;
        public SettingsManager settingsManager;

        private List<Process> systemProcesses = new List<Process>();
        private List<Process> highProcesses = new List<Process>();
        private List<Process> mediumProcesses = new List<Process>();
        private List<Process> lowProcesses = new List<Process>();

        public void AddToSystemProcesses(Process process) => systemProcesses.Add(process);
        public void AddToHighProcesses(Process process) => highProcesses.Add(process);
        public void AddToMediumProcesses(Process process) => mediumProcesses.Add(process);
        public void AddToLowProcesses(Process process) => lowProcesses.Add(process);

        protected override void BeforeRun()
        {
            Instance = this;
            settingsManager = new SettingsManager();
            console = new TextModeConsole();
        }

        protected override void Run()
        {
            foreach (Process process in systemProcesses) process.Update();
            foreach (Process process in highProcesses) process.Update();
            foreach (Process process in mediumProcesses) process.Update();
            foreach (Process process in lowProcesses) process.Update();
        }
    }
}
