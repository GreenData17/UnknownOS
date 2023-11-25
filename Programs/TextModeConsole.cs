using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cosmos.System;
using Cosmos.System.ExtendedASCII;
using UnknownOS.Core;
using Console = System.Console;

namespace UnknownOS.Programs
{
    public class TextModeConsole : Process
    {
        private Dictionary<string, string> _variables = new Dictionary<string, string>();
        private int _currentDisk = 0;
        private string _currentPath = @"\";


        private const int WIDTH = 80;
        private const int HEIGHT = 24;

        private List<string> _output;

        public string prefix;
        public string input;
        public int cursorX = 0;
        public int cursorY = 0;

        public TextModeConsole() : base("TextModeConsole", PriorityLevel.System) { }

        public override void Start()
        {
            _output = new List<string>();
            _output.Add("");
            prefix = "> ";
            input = "";

            _variables.Add("OS_NAME", Kernel.Instance.settingsManager.Settings["global.system.name"]);
            _variables.Add("OS_VERSION", Kernel.Instance.settingsManager.Settings["global.system.version"]);
            Kernel.Instance.settingsManager.AddSetting("global.console.session1.path", _currentDisk + ":" + _currentPath);

            // Setup Console
            Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
            Console.InputEncoding = Encoding.GetEncoding(437);
            Console.OutputEncoding = Encoding.GetEncoding(437);
            Console.CursorSize = 20;

            // Prepare console output
            Console.Clear();
            Console.Write(prefix);
        }

        public override void Update()
        {
            // KeyEvent manager

            if (!KeyboardManager.TryReadKey(out var keyEvent)) return;

            // Special key Events
            if (keyEvent.Key == ConsoleKeyEx.Escape)
            {
                Power.Shutdown();
            }
            else
            if (keyEvent.Key == ConsoleKeyEx.Enter)
            {
                return;
            }
            else
            if (keyEvent.Key == ConsoleKeyEx.Backspace)
            {
                BackSpace();
                return;
            }
            else
            if (keyEvent.Key == ConsoleKeyEx.LeftArrow)
            {
                if (cursorX == 0) return;

                cursorX--;
                Console.SetCursorPosition(prefix.Length + cursorX, cursorY);
                return;
            }
            else
            if (keyEvent.Key == ConsoleKeyEx.RightArrow)
            {
                if (cursorX == input.Length) return;

                cursorX++;
                Console.SetCursorPosition(prefix.Length + cursorX, cursorY);
                return;
            }

            DrawInput(keyEvent.KeyChar);
        }

        private void DrawInput(char key)
        {
            if (cursorX < input.Length)
            {
                string preInput = input.Substring(0, cursorX);
                string afterInput = input.Substring(cursorX);
                input = preInput + key + afterInput;

                Console.SetCursorPosition(0, cursorY);
                Console.Write(prefix + input + " ");
                cursorX++;
                Console.SetCursorPosition(prefix.Length + cursorX, cursorY);
                return;
            }

            input += key.ToString();

            Console.SetCursorPosition(prefix.Length + cursorX, cursorY);
            Console.Write(key.ToString());
            cursorX++;
        }

        private void BackSpace()
        {
            string preInput = input.Substring(0, cursorX - 1);
            input = preInput + input.Substring(cursorX);
            Console.SetCursorPosition(0, cursorY);
            Console.Write(prefix + input + " ");
            cursorX--;
            Console.SetCursorPosition(prefix.Length + cursorX, cursorY);
        }
    }
}
