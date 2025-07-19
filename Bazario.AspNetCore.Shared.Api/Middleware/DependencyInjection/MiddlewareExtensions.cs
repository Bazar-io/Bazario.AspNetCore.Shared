using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Bazario.AspNetCore.Shared.Api.Middleware.DependencyInjection
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Registers the <see cref="ExceptionHandlingMiddleware"/> in the application's request pipeline.
        /// </summary>
        /// <param name="builder">Application builder to which the middleware will be added.</param>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        /// <summary>
        /// Registers the <see cref="RequestLogContextMiddleware"/> in the application's request pipeline.
        /// </summary>
        /// <remarks>
        /// Must be called before <see cref="SerilogApplicationBuilderExtensions.UseSerilogRequestLogging(IApplicationBuilder, Action{Serilog.AspNetCore.RequestLoggingOptions}?)"/>
        /// </remarks>
        /// <param name="builder">Application builder to which the middleware will be added.</param>
        public static IApplicationBuilder UseRequestLogContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogContextMiddleware>();
        }
    }
}
