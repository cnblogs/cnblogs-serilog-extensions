using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.Hosting;

/// <summary>
/// Extends <see cref="IHostBuilder"/> with Serilog configuration methods.
/// </summary>
public static class SerilogHostBuilderExtensions
{
    /// <summary>
    /// Sets Serilog as the logging provider.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="logger">The Serilog logger; if not supplied, the static <see cref="Serilog.Log"/> will be used.</param>
    /// <param name="dispose">When <c>true</c>, dispose <paramref name="logger"/> when the framework disposes the provider. If the
    /// logger is not specified but <paramref name="dispose"/> is <c>true</c>, the <see cref="Serilog.Log.CloseAndFlush()"/> method will be
    /// called on the static <see cref="Log"/> class instead.</param>
    /// <param name="providers">A <see cref="LoggerProviderCollection"/> registered in the Serilog pipeline using the
    /// <c>WriteTo.Providers()</c> configuration method, enabling other <see cref="Microsoft.Extensions.Logging.ILoggerProvider"/>s to receive events. By
    /// default, only Serilog sinks will receive events.</param>
    /// <returns>The host builder.</returns>
    public static IHostBuilder AddSerilog(
        this IHostBuilder builder,
        Serilog.ILogger logger = null,
        bool dispose = false,
        LoggerProviderCollection providers = null)
    {
        builder.ConfigureLogging(logging => logging.AddSerilog(logger, dispose, providers));
        return builder;
    }

    /// <summary>Sets Serilog as the logging provider.</summary>
    /// <remarks>
    /// A <see cref="ILoggingBuilder"/> is supplied so that configuration and hosting information can be used.
    /// The logger will be shut down when application services are disposed.
    /// </remarks>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configureLogger">The delegate for configuring the <see cref="Serilog.LoggerConfiguration" /> that will be used to construct a <see cref="Serilog.Core.Logger" />.</param>
    /// <param name="preserveStaticLogger">Indicates whether to preserve the value of <see cref="Serilog.Log.Logger"/>.</param>
    /// <param name="writeToProviders">By default, Serilog does not write events to <see cref="ILoggerProvider"/>s registered through
    /// the Microsoft.Extensions.Logging API. Normally, equivalent Serilog sinks are used in place of providers. Specify
    /// <c>true</c> to write events to all providers.</param>
    /// <returns>The host builder.</returns>
    public static IHostBuilder AddSerilog(
        this IHostBuilder builder,
        Action<IConfiguration, LoggerConfiguration> configureLogger,
        bool preserveStaticLogger = false,
        bool writeToProviders = false)
    {
        builder.ConfigureLogging(logging => logging.AddSerilog(configureLogger, preserveStaticLogger, writeToProviders));
        return builder;
    }

    /// <summary>Sets Serilog as the logging provider.</summary>
    /// <remarks>
    /// A <see cref="ILoggingBuilder"/> is supplied so that configuration and hosting information can be used.
    /// The logger will be shut down when application services are disposed.
    /// </remarks>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configureLogger">The delegate for configuring the <see cref="Serilog.LoggerConfiguration" /> that will be used to construct a <see cref="Serilog.Core.Logger" />.</param>
    /// <param name="preserveStaticLogger">Indicates whether to preserve the value of <see cref="Serilog.Log.Logger"/>.</param>
    /// <param name="writeToProviders">By default, Serilog does not write events to <see cref="ILoggerProvider"/>s registered through
    /// the Microsoft.Extensions.Logging API. Normally, equivalent Serilog sinks are used in place of providers. Specify
    /// <c>true</c> to write events to all providers.</param>
    /// <returns>The host builder.</returns>
    public static IHostBuilder AddSerilog(
        this IHostBuilder builder,
        Action<LoggerConfiguration> configureLogger,
        bool preserveStaticLogger = false,
        bool writeToProviders = false)
    {
        builder.ConfigureLogging(logging => logging.AddSerilog(configureLogger, preserveStaticLogger, writeToProviders));
        return builder;
    }

    /// <summary>Sets Serilog as the logging provider.</summary>
    /// <remarks>
    /// A <see cref="ILoggingBuilder"/> is supplied so that configuration and hosting information can be used.
    /// The logger will be shut down when application services are disposed.
    /// </remarks>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="configureLogger">The delegate for configuring the <see cref="Serilog.LoggerConfiguration" /> that will be used to construct a <see cref="Serilog.Core.Logger" />.</param>
    /// <param name="preserveStaticLogger">Indicates whether to preserve the value of <see cref="Serilog.Log.Logger"/>.</param>
    /// <param name="writeToProviders">By default, Serilog does not write events to <see cref="ILoggerProvider"/>s registered through
    /// the Microsoft.Extensions.Logging API. Normally, equivalent Serilog sinks are used in place of providers. Specify
    /// <c>true</c> to write events to all providers.</param>
    /// <remarks>If the static <see cref="Log.Logger"/> is a bootstrap logger (see
    /// <c>LoggerConfigurationExtensions.CreateBootstrapLogger()</c>), and <paramref name="preserveStaticLogger"/> is
    /// not specified, the the bootstrap logger will be reconfigured through the supplied delegate, rather than being
    /// replaced entirely or ignored.</remarks>
    /// <returns>The host builder.</returns>
    public static IHostBuilder AddSerilog(
             this IHostBuilder builder,
             Action<IConfiguration, IServiceProvider, LoggerConfiguration> configureLogger,
             bool preserveStaticLogger = false,
             bool writeToProviders = false)
    {
        builder.ConfigureLogging(logging => logging.AddSerilog(configureLogger, preserveStaticLogger, writeToProviders));
        return builder;
    }
}