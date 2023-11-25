using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
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


            PrintDebug("SYSTEM", ConsoleColor.Green, "  Filesystem Starting...");
            InitFileSystem();
            PrintDebug("DEBUG", ConsoleColor.Cyan, "   Detected Disk Count: " + VFSManager.GetDisks().Count);
            PrintDiskData();
            PrintDebug("SYSTEM", ConsoleColor.Green, "  FileSystem Started!");


            PrintDebug("SYSTEM", ConsoleColor.Green, "  Starting default apps...");
            Instantiate(new TextModeConsole());


            PrintDebug("DEBUG", ConsoleColor.Cyan, "   Waiting for input to continue...");
            Console.ReadLine();
            Console.Clear();

            Destroy();
        }

        private void PrintDiskData()
        {
            // Even though there are two?! disks, it crashes if you try to get disks without registering at least one. WTF!

            for (int i = 0; i < VFSManager.GetDisks().Count; i++)
            {
                Console.WriteLine("[ INTERNAL ] Disk " + i + ": ");
                VFSManager.GetDisks()[i].DisplayInformation();
            }
        }

        private void InitFileSystem()
        {
            CosmosVFS VirtualFileSystem = new CosmosVFS();
            VFSManager.RegisterVFS(VirtualFileSystem);
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
