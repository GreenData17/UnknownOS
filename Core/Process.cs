﻿using System;
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


        private void Setup() => Kernel.Instance.AddProcess(this);
        public PriorityLevel GetPriorityLevel() => _priority;


        public virtual void Update() { }
    }
}
