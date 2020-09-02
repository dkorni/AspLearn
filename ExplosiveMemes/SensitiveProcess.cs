using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;

namespace ExplosiveMemes
{
    class SensitiveProcess
    {
        private Dictionary<int, string> _phraseStore = new Dictionary<int, string>();
        private Dictionary<int, string> _stickerStore = new Dictionary<int, string>();
        private TelegramBotClient _client;

        private Logger logger = LogManager.GetCurrentClassLogger();

        public SensitiveProcess(string phrasesJsonFile)
        {
            // loading phrases into memory
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\{phrasesJsonFile}.json");
            var json = File.ReadAllText(path);

            var phraseArray = JsonConvert.DeserializeObject<string[]>(json);

            for (int i = 0; i < phraseArray.Length; i++)
            {
                _phraseStore[i] = phraseArray[i];
            }

            // loading stickers
            var stickerPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\stickers\\{phrasesJsonFile}.json");
            var stickerJson = File.ReadAllText(stickerPath);

            var stickerArray = JsonConvert.DeserializeObject<string[]>(stickerJson);

            for (int i = 0; i < stickerArray.Length; i++)
            {
                _stickerStore[i] = stickerArray[i];
            }

            _client = Bot.Get().Result;
        }

        public async Task Process(float emotionalValue, Message message)
        {
            if (emotionalValue > 0.7)
            {
                await SendSticker(message);

                var messageSendRandom = new Random().Next(-40,100);

                if (messageSendRandom > 0)
                    await SendMessage(message);
            }
            else
            {
                await SendMessage(message);
            }
        }

        private async Task SendMessage(Message message)
        {
            // select randomly phrase
            var phraseRandom = new Random().Next(0, _phraseStore.Count);

            // send phrase to chat
            await _client.SendTextMessageAsync(message.Chat.Id, _phraseStore[phraseRandom]);

            logger.Warn($"Response text to {message.Chat.Username}: {_phraseStore[phraseRandom]}");
        }

        private async Task SendSticker(Message message)
        {
            // select randomly sticker
            var stickerRandom = new Random().Next(0, _stickerStore.Count);

            // send phrase to chat
            await _client.SendStickerAsync(message.Chat.Id, _stickerStore[stickerRandom]);

            logger.Warn($"Response sticker to {message.Chat.Username}: {_stickerStore[stickerRandom]}");
        }
    }
}
