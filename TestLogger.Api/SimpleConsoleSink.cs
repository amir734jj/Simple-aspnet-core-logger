using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Models;
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
                var payload = new Dictionary<string, object>
                {
                    ["CategoryName"] = categoryName,
                    ["LogLevel"] = logLevel.ToString(),
                    ["EventId"] = eventId,
                    ["Message"] = message
                };

                switch (state)
                {
                    case HttpContextLogModel structuredLog:
                        payload["State"] = structuredLog;
                        break;
                }
                
                if (exception != null)
                {
                    payload["Exception"] = exception;
                }
                
                var json = JsonConvert.SerializeObject(payload);

                Console.WriteLine(json);
            }
        }
    }
}
