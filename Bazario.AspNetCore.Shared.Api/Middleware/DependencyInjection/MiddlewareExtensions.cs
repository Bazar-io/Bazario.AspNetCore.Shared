using Microsoft.AspNetCore.Builder;

namespace Bazario.AspNetCore.Shared.Api.Middleware.DependencyInjection
{
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
