using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.CreateOwnServices.services;

namespace Services.PassDependencies
{
    public class MessageService
    {
        private IMessageSender _sender;

        // here IMessageSender dependency
        // is passed to constructor
        public MessageService(IMessageSender sender)
        {
            _sender = sender;
        }

        public string Send()
        {
            return _sender.Send();
        }
    }
}
