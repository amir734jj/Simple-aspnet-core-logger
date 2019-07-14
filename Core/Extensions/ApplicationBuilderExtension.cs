using System;
using Microsoft.AspNetCore.Builder;
using SimpleLogger.Middleware;
using SimpleLogger.Models;

namespace SimpleLogger.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSimpleContextLoggerMiddleware(this IApplicationBuilder source, Func<HttpContextLogModel, object> formatter = null)
        {
            // ReSharper disable once ArrangeRedundantParentheses
            formatter = formatter ?? (x => x);
            
            source.UseMiddleware<SimpleLoggerMiddleware>(formatter);
        }
    }
}