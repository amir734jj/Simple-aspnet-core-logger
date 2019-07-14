using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using SimpleLogger.Extensions;
using SimpleLogger.Interfaces;

namespace SimpleLogger.Logic
{
    public class SimpleLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, SimpleLogger> _table;
        private readonly ISimpleSink _simpleSink;

        public SimpleLoggerProvider(ISimpleSink simpleSink)
        {
            _table = new ConcurrentDictionary<string, SimpleLogger>();
            _simpleSink = simpleSink;
        }
        
        public void Dispose()
        {
            // Dispose individual loggers
            _table.ForEach(x => x.Value.Dispose());
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _table
                .AddOrUpdate(categoryName, _ => new SimpleLogger(categoryName, _simpleSink), (existing, logger) => _table[existing] = logger);
        }
    }
}