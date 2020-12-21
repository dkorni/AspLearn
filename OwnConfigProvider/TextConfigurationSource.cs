using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OwnConfigProvider
{
    public class TextConfigurationSource : IConfigurationSource
    {

        public string FilePath { get; private set; }

        public TextConfigurationSource(string filePath)
        {
            FilePath = filePath;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var filePath = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            return new TextConfigurationProvider(filePath);
        }
    }
}
