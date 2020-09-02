using NLog;
using System;
using System.Threading.Tasks;
using ExplosiveMemes.Services;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class SendBear : ICommand
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        private BearService _bearService;

        public static string Name
        {
            get => "🍺";
        }

        public SendBear(BearService bearService)
        {
            _bearService = bearService;
        }

        public async Task Execute(Message message)
        {
            var respnse = "Спасиба, брат!!!";
            var bot = await Bot.Get();
            var sticker = "CAACAgIAAxkBAAEBF_JfGw14NvrRaphn7je6Ku3ZT5wmgQACNcsAAmOLRgzVH3jFvLg04hoE";

            await bot.SendStickerAsync(message.Chat.Id,
                sticker);
            await bot.SendTextMessageAsync(message.Chat.Id, respnse);
            logger.Warn($"Response to {message.Chat.Username}: {respnse}");

            logger.Warn($"Response text to {message.Chat.Username}: {respnse}");

            logger.Warn($"Response sticker to {message.Chat.Username}: {sticker}");

            await _bearService.UpdateLastBear(message.Chat.Username);
        }

        public Task<T> Execute<T>(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
