using System;
using Microsoft.Extensions.Logging;
using SimpleLogger.Interfaces;
using SimpleLogger.Utilities;

namespace SimpleLogger.Logic
{
    public class SimpleLogger : ILogger, IDisposable
    {
        private readonly string _categoryName;
        private readonly ISimpleSink _simpleSink;

        public SimpleLogger(string categoryName, ISimpleSink simpleSink)
        {
            _categoryName = categoryName;
            _simpleSink = simpleSink;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _simpleSink.Emit(_categoryName, logLevel, eventId, state, exception, formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return Disposable.Empty;
        }

        public void Dispose()
        {
            // Nothing needs to be done
        }
    }
}
