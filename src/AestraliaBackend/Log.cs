using Microsoft.Extensions.Logging;

public static class Log
{
    public static ILogger Logger { get; private set; } = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("default Logger");

    public static void Configure(ILoggerFactory? factory, string name)
    {
        factory ??= LoggerFactory.Create(builder => builder.AddConsole());
        Logger = factory.CreateLogger(name);
    }
}
