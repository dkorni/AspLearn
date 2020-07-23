using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public abstract class Command
    {
        public virtual async Task Execute(Message message) => await Task.Run(()=>Console.WriteLine("It is command"));
    }
}
