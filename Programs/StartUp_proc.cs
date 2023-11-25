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
            PrintDebug("SYSTEM", ConsoleColor.Green, "  System starting...");
            PrintDebug("SYSTEM", ConsoleColor.Green, "  System started!");


            PrintDebug("SYSTEM", ConsoleColor.Green, "  Starting Filesystem...");
            // TODO: Start Filesystem Process here!
            PrintDebug("ERROR", ConsoleColor.Red, "   Filesystem is not implemented yet!");


            PrintDebug("SYSTEM", ConsoleColor.Green, "  Starting default apps...");
            Instantiate(new TextModeConsole());


            PrintDebug("DEBUG", ConsoleColor.Cyan, "   Waiting for input to continue...");
            Console.ReadLine();
            Console.Clear();

            Destroy();
        }

        private void PrintDebug(string prefix, ConsoleColor prefixColor, string msg)
        {
            Console.Write("[ ");
            Console.ForegroundColor = prefixColor;
            Console.Write(prefix);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ]");

            Console.Write(msg);
            Console.WriteLine();
        }
    }
}
