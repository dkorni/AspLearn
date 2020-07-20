using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CreateOwnServices.services
{
    class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Send by email";
        }
    }
}
