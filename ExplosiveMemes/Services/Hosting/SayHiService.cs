using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ExplosiveMemes.Services.Hosting
{
    public class SayHiService : IHostedService, IDisposable
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private Timer _timer;

        private TelegramBotClient _client;

        private Dictionary<int, string> _phraseStore = new Dictionary<int, string>();
        private Dictionary<int, string> _stickerStore = new Dictionary<int, string>();

        private TimeSpan _maxHours = TimeSpan.FromHours(24);

        public Task StartAsync(CancellationToken cancellationToken)
        {

            // loading phrases into memory
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\speech.json");
            var json = File.ReadAllText(path);

            var phraseArray = JsonConvert.DeserializeObject<string[]>(json);

            for (int i = 0; i < phraseArray.Length; i++)
            {
                _phraseStore[i] = phraseArray[i];
            }

            // loading stickers
            var stickerPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\stickers\\speech.json");
            var stickerJson = File.ReadAllText(stickerPath);

            var stickerArray = JsonConvert.DeserializeObject<string[]>(stickerJson);

            for (int i = 0; i < stickerArray.Length; i++)
            {
                _stickerStore[i] = stickerArray[i];
            }

            _client = Bot.Get().Result;

            _logger.Trace("()");

            _timer = new Timer(SayHello, null, TimeSpan.Zero,
                TimeSpan.FromHours(6));

            _client = Bot.Get().Result;

            return Task.CompletedTask;
        }

        private void SayHello(object state)
        {
            using (var dbContext = new DatabaseContext())
            {
                var users = dbContext.Users.ToList().Where(u => (DateTime.UtcNow - u.LastMessage) > _maxHours);

                foreach (var user in users)
                {
                   // send hello message
                   
                   var phraseRandom = new Random().Next(0, _phraseStore.Count);
                   var stickerRandom = new Random().Next(0, _stickerStore.Count);

                   Task.Run(async () =>
                   {

                       await _client.SendStickerAsync(new ChatId(Int32.Parse(user.ChatId)),
                           _stickerStore[stickerRandom]);
                       await _client.SendTextMessageAsync(new ChatId(Int32.Parse(user.ChatId)),
                               _phraseStore[phraseRandom]);

                       _logger.Warn($"Response text to {user.Username}: {_phraseStore[phraseRandom]}");
                       _logger.Warn($"Response sticker to {user.Username}: {_stickerStore[stickerRandom]}");
                   });

                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}