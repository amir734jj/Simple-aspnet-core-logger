using System;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TestLogger.Api
{
    public class SimpleConsoleSink : ISimpleSink
    {
        public void Emit<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception,
            string message)
        {
            // Log only information
            if (logLevel == LogLevel.Information)
            {
                var payload = new
                {
                    CategoryName = categoryName,
                    LogLevel = logLevel,
                    EventId = eventId,
                    State = state,
                    Exception = exception
                };

                var json = JsonConvert.SerializeObject(payload);

                Console.WriteLine(json);
            }
        }
    }
}