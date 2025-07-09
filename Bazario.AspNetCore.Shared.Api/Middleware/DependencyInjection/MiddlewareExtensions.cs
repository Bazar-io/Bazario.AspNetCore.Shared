using Microsoft.AspNetCore.Builder;

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
    }
}
