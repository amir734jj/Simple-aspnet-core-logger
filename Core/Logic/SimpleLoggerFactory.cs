using Microsoft.Extensions.Logging;

namespace Core
{
    public class SimpleLoggerFactory : ILoggerFactory
    {
        private ILoggerProvider _provider;

        public void Dispose()
        {
            // Do not manually dispose the ILoggerProvider
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _provider.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            _provider = provider;
        }
    }
}