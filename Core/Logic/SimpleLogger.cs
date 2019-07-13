using System;
using Core.Interfaces;
using Core.Utilities;
using Microsoft.Extensions.Logging;

namespace Core
{
    class ConsoleSink : ISimpleSink
    {
        public void Emit<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception,
            string message)
        {
            throw new NotImplementedException();
        }
    }
    
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