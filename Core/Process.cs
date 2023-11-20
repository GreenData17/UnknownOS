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

        private PriorityLevel _priority;
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

        public string name = "New Process";
        private bool _hasStarted = false;
        public bool hasStarted { get => _hasStarted; }


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
        public void Destroy() => Kernel.Instance.RemoveProcess(this);


        public void TriggerUpdate()
        {
            if (!hasStarted) InternalStart();
            Update();
        }
        private void InternalStart()
        {
            Start();
            _hasStarted = true;
        }

        public virtual void Update() { }
        public virtual void Start() { }


        /// <summary>
        /// Creates a new Process and adds it in the runtime loop.
        /// </summary>
        /// <param name="process"></param>
        public static void Instantiate(Process process) => Kernel.Instance.AddProcess(process);
    }
}
