using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;
using Xunit;

namespace Serilog.Extensions.Hosting.Tests;

public class SerilogLoggingBuilderExtensionsTests
{
    [Fact]
    public async Task LoggingBuilderExtensions_AddSerilog_SuccessAsync()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var logger = new LoggerConfiguration()
            .WriteTo.InMemory()
            .CreateLogger();
        builder.Logging.AddSerilog(logger);
        builder.WebHost.UseTestServer();
        var app = builder.Build();

        // Act
        var message = "Hello World!";
        app.Logger.LogInformation("Hello World!");
        await app.StartAsync();

        // Assert
        InMemorySink.Instance.Should().HaveMessage(message);
    }
}