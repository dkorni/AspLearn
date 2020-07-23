using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class StartCommand :Command
    {
        public override async Task Execute(Message message)
        {
            var client = await Bot.Get();

           

            //client.SendChatActionAsync(message.Chat.Id)
        }
    }
}
