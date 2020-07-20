using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CreateOwnServices.services
{
    public interface IMessageSender
    {
        string Send();
    }
}
