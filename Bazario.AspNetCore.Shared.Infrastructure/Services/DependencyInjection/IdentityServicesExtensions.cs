using Bazario.AspNetCore.Shared.Application.Abstractions.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Infrastructure.Services.DependencyInjection
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddUserContextServiceWithHttpContextAccessor(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextService, UserContextService>();

            return services;
        }
    }
}
