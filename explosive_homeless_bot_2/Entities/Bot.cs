using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace explosive_homeless_bot_2.Entities
{
    public class Bot
    {
        public static TelegramBotClient Client;

        public static async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (Client != null)
            {
                return Client;
            }


            Client = new TelegramBotClient("1092182529:AAF5Oo1CQHnAsPaVQGnL1Rt5iwEiX4itNOc");
            string hook = string.Format("https://explosivehomelessbot220200713113207.azurewebsites.net/", "api/bot");
            await Client.SetWebhookAsync(hook);
            return Client;
        }
    }
}
