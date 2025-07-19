using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Bazario.AspNetCore.Shared.Api.Middleware
{
    /// <summary>
    /// Middleware for adding a correlation ID to the log context for each request.
    /// </summary>
    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to add a correlation ID to the log context.
        /// </summary>
        /// <param name="context">
        /// The <see cref="HttpContext"/> for the current request. This context is used to
        /// add the correlation ID to the log context, which can be used for tracking requests in logs.
        /// </param>
        public async Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                await _next(context);
            }
        }
    }
}
