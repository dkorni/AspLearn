using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace OwnLogProvider
{
    public class TextLogger : ILogger
    {
        private static object _lock = new object();
        private string _path;

        public TextLogger(string path)
        {
            _path = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(_path, logLevel+": "+formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}
