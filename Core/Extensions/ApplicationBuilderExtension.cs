using System;
using Core.Middleware;
using Core.Models;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
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