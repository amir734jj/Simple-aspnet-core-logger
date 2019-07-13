using Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Core.Extensions
{
    public static class WebHostBuilderExtension
    {
        /// <summary>
        /// WebHostBuilder extension to add SimpleLog
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseSimpleLog<TSimpleSink>(
            this IWebHostBuilder builder) where TSimpleSink : class, ISimpleSink
        {
            return builder.ConfigureServices((x, y) =>
            {
                y.AddSingleton<ISimpleSink>(ctx => ctx.GetRequiredService<TSimpleSink>());
                y.AddSingleton<ILoggerProvider>(ctx => ctx.GetRequiredService<SimpleLoggerProvider>());
                y.AddSingleton<ILoggerFactory>(ctx => ctx.GetRequiredService<SimpleLoggerFactory>());
            });
        }
    }
}