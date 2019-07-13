using System;
using Microsoft.Extensions.Logging;

namespace Core.Interfaces
{
    public interface ISimpleSink
    {
        void Emit<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, string message);
    }
}