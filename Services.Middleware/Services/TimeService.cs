using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CreateOwnServices.services
{
    public class TimeService
    {
        public string Time { get; }

        public TimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
