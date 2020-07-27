using System;
using System.Threading.Tasks;
using ExplosiveMemes.Services;
using Telegram.Bot.Types;

namespace ExplosiveMemes.Commands
{
    public class Advice : ICommand
    {
        private PhraseStore _phraseStore;
        private Random _random = new Random();

        public Advice(PhraseStore phraseStore)
        {
            _phraseStore = phraseStore;
        }

        public static string Name => "Совет";

        public async Task Execute(Message message)
        {
            var id =_random.Next(0, _phraseStore.Count-1);
            _phraseStore.TryToGet(id, out var phrase);
            var bot = await Bot.Get();
            await bot.SendTextMessageAsync(message.Chat.Id, phrase);
        }
    }
}
