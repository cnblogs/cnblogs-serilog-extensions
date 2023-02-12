using Microsoft.Extensions.DependencyInjection;
using Serilog.Extensions.Logging;
using System;
using static Serilog.SerilogHostBuilderExtensions;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Extends <see cref="ILoggingBuilder"/> with Serilog configuration methods.
    /// </summary>
    public static class SerilogLoggingBuilderExtensions
    {
        /// <summary>
        /// Sets Serilog as the logging provider.
        /// </summary>
        /// <param name="builder">The logging builder to configure.</param>
        /// <param name="logger">The Serilog logger; if not supplied, the static <see cref="Serilog.Log"/> will be used.</param>
        /// <param name="dispose">When <c>true</c>, dispose <paramref name="logger"/> when the framework disposes the provider. If the
        /// logger is not specified but <paramref name="dispose"/> is <c>true</c>, the <see cref="Serilog.Log.CloseAndFlush()"/> method will be
        /// called on the static <see cref="Serilog.Log"/> class instead.</param>
        /// <param name="providers">A <see cref="LoggerProviderCollection"/> registered in the Serilog pipeline using the
        /// <c>WriteTo.Providers()</c> configuration method, enabling other <see cref="Microsoft.Extensions.Logging.ILoggerProvider"/>s to receive events. By
        /// default, only Serilog sinks will receive events.</param>
        /// <returns>The host builder.</returns>
        public static ILoggingBuilder AddSerilog(
            this ILoggingBuilder builder,
            Serilog.ILogger logger = null,
            bool dispose = false,
            LoggerProviderCollection providers = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var services = builder.Services;
            if (providers != null)
            {
                services.AddSingleton<ILoggerFactory>(services =>
                {
                    var factory = new SerilogLoggerFactory(logger, dispose, providers);

                    foreach (var provider in services.GetServices<ILoggerProvider>())
                        factory.AddProvider(provider);

                    return factory;
                });
            }
            else
            {
                services.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, dispose));
            }

            if (logger != null)
            {
                // This won't (and shouldn't) take ownership of the logger.
                services.AddSingleton(logger);

                // Still need to use RegisteredLogger as it is used by ConfigureDiagnosticContext.
                services.AddSingleton(new RegisteredLogger(logger));
            }
            bool useRegisteredLogger = logger != null;
            ConfigureDiagnosticContext(services, useRegisteredLogger);

            return builder;
        }
    }
}