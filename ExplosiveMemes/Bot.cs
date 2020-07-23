using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ExplosiveMemes
{
    public static class Bot
    {
        private static TelegramBotClient _client;

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
            {
                return _client;
            }

            _client = new TelegramBotClient(AppSettings.Key);
            await _client.SetWebhookAsync(string.Format(AppSettings.ServerUrl, "api/message/update"));
            return _client;
        }
    }
}
