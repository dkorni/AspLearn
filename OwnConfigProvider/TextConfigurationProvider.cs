using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OwnConfigProvider
{
    public class TextConfigurationProvider : ConfigurationProvider
    {
        public string FiletPath { get; set; }

        public TextConfigurationProvider(string path)
        {
            FiletPath = path;
        }

        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var fs = new FileStream(FiletPath, FileMode.Open))
            {
                using (var textReader = new StreamReader(fs))
                {
                    string line;
                    while ((line = textReader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        string value = textReader.ReadLine();
                        data.Add(line, value);
                    }
                }
            }

            Data = data;
        }
    }
}
