using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.DependencyInjection
{
    public static class InterceptorsExtensions
    {
        public static IServiceCollection RegisterInterceptor<TInterceptor>(
            this IServiceCollection services)
            where TInterceptor : class, IInterceptor
        {
            return services.AddScoped<TInterceptor>();
        }
    }
}
