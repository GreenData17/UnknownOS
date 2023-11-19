using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnknownOS.Core
{
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

        public Process(PriorityLevel priorityLevel)
        {
            _priority = priorityLevel;
            Setup();
        }

        public Process()
        {
            _priority = PriorityLevel.Low;
            Setup();
        }

        private void Setup()
        {
            if( _priority == PriorityLevel.System ) Kernel.Instance.AddToSystemProcesses(this);
            if( _priority == PriorityLevel.High ) Kernel.Instance.AddToHighProcesses(this);
            if( _priority == PriorityLevel.Medium ) Kernel.Instance.AddToMediumProcesses(this);
            if( _priority == PriorityLevel.Low ) Kernel.Instance.AddToLowProcesses(this);
        }

        public virtual void Update() { }
    }
}
