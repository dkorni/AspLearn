using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExplosiveMemes.Utils;
using NLog;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class StartCommand : ICommand
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public static string Name => "/start";

        public async Task Execute(Message message)
        {
            var client = await Bot.Get();

            var response = "Здорова братишь, пить будешь🍾?";

            await ApiProvider.DisplayMenu(message.Chat.Id,  response,true, "Да", "Нет");

            logger.Warn($"Response to {message.Chat.Username}: {response}");

            //client.SendChatActionAsync(message.Chat.Id)
        }

        public Task<T> Execute<T>(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
