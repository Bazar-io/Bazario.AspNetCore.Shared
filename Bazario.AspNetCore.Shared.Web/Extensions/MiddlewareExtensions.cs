using Bazario.AspNetCore.Shared.Web.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Bazario.AspNetCore.Shared.Web.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
