using Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSimpleContextLoggerMiddleware(this IApplicationBuilder source)
        {
            source.UseMiddleware<SimpleLoggerMiddleware>();
        }
    }
}