using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;
using Xunit;

namespace Cnblogs.Serilog.Extensions.Tests;

public class SerilogLoggingBuilderExtensionsTests
{
    [Fact]
    public async Task LoggingBuilderExtensions_AddSerilog_SuccessAsync()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        builder.Logging.AddSerilog(logger => logger.WriteTo.InMemory());
        builder.WebHost.UseTestServer();
        var app = builder.Build();

        // Act
        var message = "Logging in memory";
        app.Logger.LogInformation(message);
        await app.StartAsync();

        // Assert
        InMemorySink.Instance.Should().HaveMessage(message);
    }

    [Fact]
    public async Task LoggingBuilderExtensions_AddSerilogWithAppsettings_SuccessAsync()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        builder.Logging.AddSerilog((appConfig, loggerConfig) => loggerConfig.ReadFrom.Configuration(appConfig));
        builder.WebHost.UseTestServer();
        var app = builder.Build();

        // Act
        var message = "Logging in memory with appsettings.json";
        app.Logger.LogInformation(message);
        await app.StartAsync();

        // Assert
        InMemorySink.Instance.Should().HaveMessage(message);
    }
}