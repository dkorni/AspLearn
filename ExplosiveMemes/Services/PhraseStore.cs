using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ExplosiveMemes.Services
{
    public class PhraseStore
    {
        public int Count
        {
            get => _phrases.Count;
        }

        private Dictionary<int, string> _phrases = new Dictionary<int, string>();

        public PhraseStore()
        {
            LoadPhrases();
        }

        /// <summary>
        /// Try to get command instance, if command is supported
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public bool TryToGet(int id, out string phrase)
        {
            var result = _phrases.TryGetValue(id, out phrase);

            return result;
        }

        /// <summary>
        /// Loads the command types.
        /// </summary>
        private void LoadPhrases()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\json\\phrases.json");
            var json = File.ReadAllText(path);

            var phraseArray = JsonConvert.DeserializeObject<string[]>(json);

            for (int i = 0; i < phraseArray.Length; i++)
            {
                _phrases[i] = phraseArray[i];
            }

        }
    }
}
