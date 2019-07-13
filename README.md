## Simple ASPNET Core logger

`ILogger`, `IloggerProvider`, `ILoggerFactory` implementation with HttpContextLogger middleware + Console/JSON logger.

1) To use this customizable logger, implement `ISimpleSink`:

```csharp
public class SimpleConsoleSink : ISimpleSink
{
    public void Emit<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception, string message)
    {
        throw new NotImplementedException();
    }
}
```

2) Then add it to `IWebHostBuilder`:
```csharp
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseSimpleLog<SimpleConsoleSink>(); // <-- Add this line;
```

3) HttpContext logger:

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    
    // Middleware to log HttpContext
    app.UseSimpleContextLoggerMiddleware();
    
    app.UseMvc();
    ...
}
```
