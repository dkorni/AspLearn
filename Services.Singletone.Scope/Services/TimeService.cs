using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Singletone.Scope.Services
{
    public class TimeService
    {
        private ITimer _timer;

        public TimeService(ITimer timer)
        {
            _timer = timer;
        }

        public string GetTime()
        {
            return _timer.Time;
        }
    }
}
