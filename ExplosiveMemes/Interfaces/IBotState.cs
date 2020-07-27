using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Interfaces
{
    public interface IBotState
    {
        void Execute(Message message);
    }
}
