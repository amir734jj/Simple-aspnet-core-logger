using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleLogger.Interfaces;
using SimpleLogger.Logic;

namespace SimpleLogger.Extensions
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
                y.AddSingleton<ISimpleSink, TSimpleSink>();
                y.AddSingleton<ILoggerProvider, SimpleLoggerProvider>();
                y.AddSingleton<ILoggerFactory, SimpleLoggerFactory>(ctx =>
                {
                    var provider = ctx.GetService<ILoggerProvider>();
                    var instance = new SimpleLoggerFactory();
                    
                    // Add provider immediately after instantiation
                    instance.AddProvider(provider);
                    return instance;
                });
            });
        }
    }
}