using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Bazario.AspNetCore.Shared.Api.Logging.DependencyInjection
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// Configures Serilog logging for the application.
        /// </summary>
        /// <remarks>
        /// Call <see cref="SerilogApplicationBuilderExtensions.UseSerilogRequestLogging(IApplicationBuilder, Action{Serilog.AspNetCore.RequestLoggingOptions}?)"/>
        /// on the <see cref="IApplicationBuilder"/> to enable request logging in the application pipeline.
        /// </remarks>
        /// <param name="host">The <see cref="IHostBuilder"/> to configure.</param>
        public static IHostBuilder ConfigureSerilogFromConfiguration(this IHostBuilder host)
        {
            host.UseSerilog((context, configuration) =>
            {
                var loggerConfig = configuration.ReadFrom.Configuration(context.Configuration);
            });

            return host;
        }
    }
}
