using Cnblogs.Serilog.Extensions.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Xunit;

namespace Cnblogs.Serilog.Extensions.Tests
{
    public class LoggerSettingsConfigurationExtensionsTests
    {
        [Fact]
        public void SinksAreInjectedFromTheServiceProvider()
        {
            var emittedEvents = new List<LogEvent>();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ILogEventSink>(new ListSink(emittedEvents));
            using var services = serviceCollection.BuildServiceProvider();

            using var logger = new LoggerConfiguration()
                .ReadFrom.Services(services)
                .CreateLogger();

            logger.Information("Hello, world!");

            var evt = Assert.Single(emittedEvents);
            Assert.Equal("Hello, world!", evt!.MessageTemplate.Text);
        }
    }
}