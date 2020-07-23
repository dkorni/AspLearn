using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExplosiveMemes.Utils
{
    public class ApiProvider
    {
        /// <summary>
        /// Displays the menu.
        /// </summary>
        /// <param name="chatId">The chat identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="isOneTime">if set to <c>true</c> [is one time].</param>
        /// <param name="buttons">The buttons.</param>
        public static async Task DisplayMenu(long chatId, string text, bool isOneTime, params string[] buttons)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $"https://api.telegram.org/bot{AppSettings.Key}/" +
                $"sendmessage?chat_id={chatId}&text={text}&reply_markup={{\"keyboard\":[");

            // append buttons
            for (int i = 0; i < buttons.Length; i++)
            {
                stringBuilder.Append($"[\"{buttons[i]}\"]");
                if (i != buttons.Length-1)
                    stringBuilder.Append(",");
            }

            // is one time
            stringBuilder.Append($"],\"one_time_keyboard\":{isOneTime.ToString().ToLower()}");
            stringBuilder.Append("}");

            // send request
           await SendHttpRequestAsync(stringBuilder.ToString());
        }

        /// <summary>
        /// Sends the HTTP request.
        /// </summary>
        /// <param name="requestUrl">The request URL.</param>
        private static async Task SendHttpRequestAsync(string requestUrl)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                   var res = await httpClient.GetAsync(requestUrl);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
