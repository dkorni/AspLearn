using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Services
{
    public static class  SensitiveProcessor
    {
        private static readonly Dictionary<string, SensitiveProcess> _processes = new Dictionary<string, SensitiveProcess>()
        {
            ["positive"] = new SensitiveProcess("positive"),
            ["neutral"] = new SensitiveProcess("neutral"),
            //["skip"] = new SensitiveProcess("skip"),
            ["negative"] = new SensitiveProcess("negative"),
            ["speech"] = new SensitiveProcess("speech"),
        };

        public static async Task Process(string emotionType, float emotionalValue, Message message)
        {
            // select process
            if (_processes.TryGetValue(emotionType, out var process))
            {
                // process
                await process.Process(emotionalValue, message);
            }
        }
    }
}
