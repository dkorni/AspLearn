using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace ExplosiveMemes.Commands
{
    public class SendBear : ICommand
    {
        public static string Name
        {
            get => "🍺";
        }

        public async Task Execute(Message message)
        {
            var bot = await Bot.Get();
            await bot.SendStickerAsync(message.Chat.Id,
                "CAACAgIAAxkBAAEBF_JfGw14NvrRaphn7je6Ku3ZT5wmgQACNcsAAmOLRgzVH3jFvLg04hoE");
            await bot.SendTextMessageAsync(message.Chat.Id, "Спасиба, брат!!!");
        }
    }
}
