using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class SensitiveRegonizeResponseCommand
    {
        public async Task<SenseResult> Execute(Message message)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"http://127.0.0.1:5000/sense/{message.Text}");

                var response = await httpClient.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Dictionary<string, float>>(json).OrderByDescending(k => k.Value)
                    .FirstOrDefault();

                return new SenseResult()
                {
                    Intonation = result.Key,
                    Value = result.Value
                };
            }
        }
    }

    public class SenseResult
    {
        public string Intonation { get; set; }
        public float Value { get; set; }
    }
}
