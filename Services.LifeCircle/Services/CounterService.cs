using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.LifeCircle.Services
{
    public class CounterService
    {
        public ICounter Counter { get; }

        public CounterService(ICounter counter)
        {
            Counter = counter;
        }
    }
}
