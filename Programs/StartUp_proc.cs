using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownOS.Core;

namespace UnknownOS.Programs
{
    internal class StartUpProc : Process
    {
        public StartUpProc() : base("StartUpProc", PriorityLevel.System) { }

        public override void Start()
        {
            Console.Clear();
            Console.WriteLine("[ SYSTEM ]  System starting...");
            Console.WriteLine("[ SYSTEM ]  System started!");


            Console.WriteLine("[ SYSTEM ]  Starting Filesystem...");
            // TODO: Start Filesystem Process here!
            Console.WriteLine("[ ERROR ]   Filesystem is not implemented yet!");


            Console.WriteLine("[ SYSTEM ]  Starting default apps...");
            Process.Instantiate(new TextModeConsole());


            Console.WriteLine("[ DEBUG ]   Waiting for input to continue...");
            Console.ReadLine();
            Console.Clear();

            Destroy();
        }
    }
}
