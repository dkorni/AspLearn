using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExplosiveMemes.Utils;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class StartCommand : ICommand
    {
        public static string Name => "/start";

        public async Task Execute(Message message)
        {
            var client = await Bot.Get();

            await ApiProvider.DisplayMenu(message.Chat.Id, "Здорова братишь, пить будешь🍾?", true, "Да", "Нет");

            //client.SendChatActionAsync(message.Chat.Id)
        }
    }
}
