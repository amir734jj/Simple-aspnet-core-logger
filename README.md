## Simple ASPNET Core logger

1) To use this customizable logger, implement `ISimpleSink`:
```
class SimpleConsoleSink : ISimpleSink
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
}
```