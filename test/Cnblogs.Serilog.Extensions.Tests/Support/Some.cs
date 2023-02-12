using Serilog;
using Serilog.Events;

namespace Cnblogs.Serilog.Extensions.Tests.Support
{
    internal static class Some
    {
        private static int _next;

        public static int Int32() => Interlocked.Increment(ref _next);

        public static string String(string tag = null) => $"s_{tag}{Int32()}";

        public static LogEventProperty LogEventProperty() => new LogEventProperty(String("name"), new ScalarValue(Int32()));

        public static ILogger Logger() => new LoggerConfiguration().CreateLogger();
    }
}