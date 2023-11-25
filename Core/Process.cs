using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownOS.Core
{
    /// <summary>
    /// The raw Process class on which every <br/> process has to be based on.
    /// </summary>
    public class Process
    {
        public enum PriorityLevel
        {
            System,
            High,
            Medium,
            Low
        }

        private int _id = 0;
        public string name = "New Process";
        private bool _hasStarted = false;
        private PriorityLevel _priority;

        private List<string> StandardOutput = new List<string>();

        // Properties

        public int id
        {
            get => _id;
            set
            {
                if (_id == 0)
                {
                    _id = value;
                }
            }
        }

        public bool hasStarted
        {
            get => _hasStarted;
        }

        private PriorityLevel priority
        {
            get => _priority;
            set
            {
                if (value == PriorityLevel.System)
                    _priority = PriorityLevel.High;
                else
                    _priority = value;
            }
        }

        // Public functions

        public Process(string name, PriorityLevel priorityLevel)
        {
            this.name = name;
            _priority = priorityLevel;
        }

        public Process()
        {
            _priority = PriorityLevel.Low;
        }


        public PriorityLevel GetPriorityLevel() => _priority;
        public void Destroy() => Kernel.Instance.processManager.RemoveProcess(this);
        public void AddOutput(string output) => StandardOutput.Add(output);

        public List<string> GetOutput()
        {
            OnStandardOutputRead();
            return StandardOutput;
        }


        // Overrides

        public void TriggerUpdate()
        {
            if (!hasStarted) InternalStart();
            Update();
        }

        // private Functions

        private void InternalStart()
        {
            Start();
            _hasStarted = true;
        }

        // Overrides

        public virtual void Update() { }
        public virtual void Start() { }

        public virtual void OnStandardOutputRead() { }

        // Static Functions

        /// <summary>
        /// Creates a new Process and adds it in the runtime loop.
        /// </summary>
        /// <param name="process"></param>

        public static void Instantiate(Process process)
        {
            Kernel.Instance.processManager.AddProcess(process);
        }
    }
}
