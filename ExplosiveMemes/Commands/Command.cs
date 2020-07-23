using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public interface ICommand
    {
        static string Name { get; }

        Task  Execute(Message message);
    }
}
