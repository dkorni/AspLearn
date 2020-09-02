using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace ExplosiveMemes.Services
{
    public class BearService
    {
        private TelegramBotClient _client = Bot.Get().Result;

        private Dictionary<int, string> _phraseStore = new Dictionary<int, string>();

        private Dictionary<int, string> _stickerStore = new Dictionary<int, string>();

        private Logger logger = LogManager.GetCurrentClassLogger();

        public BearService()
        {
            // loading phrases into memory
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\expectedBear.json");
            var json = System.IO.File.ReadAllText(path);

            var phraseArray = JsonConvert.DeserializeObject<string[]>(json);

            for (int i = 0; i < phraseArray.Length; i++)
            {
                _phraseStore[i] = phraseArray[i];
            }

            // loading stickers
            var stickerPath = Path.Combine(Directory.GetCurrentDirectory(),
                $"wwwroot\\json\\stickers\\expectedBear.json");
            var stickerJson = System.IO.File.ReadAllText(stickerPath);

            var stickerArray = JsonConvert.DeserializeObject<string[]>(stickerJson);

            for (int i = 0; i < stickerArray.Length; i++)
            {
                _stickerStore[i] = stickerArray[i];
            }
        }

        public async Task ResponseNotBearAsync(Message message)
        {
            var phraseRandom = new Random().Next(0, _phraseStore.Count);
            var stickerRandom = new Random().Next(0, _phraseStore.Count);

            var phrase = _phraseStore[phraseRandom];
            var sticker = _stickerStore[stickerRandom];

            // send
            await _client.SendStickerAsync(message.Chat.Id, sticker);
            await _client.SendTextMessageAsync(message.Chat.Id, phrase);

            logger.Warn($"Response text to {message.Chat.Username}: {phrase}");

            logger.Warn($"Response sticker to {message.Chat.Username}: {sticker}");
        }

        public async Task UpdateLastBear(string userName)
        {
            using (var context = new DatabaseContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == userName);

                user.LastBear = DateTime.UtcNow;

                await context.SaveChangesAsync();
            }
        }
    }
}
