using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OwnLogProvider
{
    public class TextLogProvider : ILoggerProvider
    {
        private string _path;

        public TextLogProvider(string path)
        {
            _path = path;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
           return new TextLogger(_path);
        }
    }
}
