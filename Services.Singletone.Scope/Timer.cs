using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Singletone.Scope
{
    public class Timer : ITimer
    {
        public Timer()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }

        public string Time { get; }
    }
}
