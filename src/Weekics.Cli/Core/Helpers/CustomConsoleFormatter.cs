using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace Weekics.Cli.Core.Helpers;

public class CustomConsoleFormatter : ConsoleFormatter
{
    public CustomConsoleFormatter() : base("custom") { }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var originalMessage = logEntry.Formatter(logEntry.State, logEntry.Exception);
        textWriter.WriteLine($"{currentTime} {originalMessage}");
    }
}