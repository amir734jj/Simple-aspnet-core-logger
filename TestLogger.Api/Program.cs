using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SimpleLogger.Extensions;

namespace TestLogger.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSimpleLog<SimpleConsoleSink>();
    }
}