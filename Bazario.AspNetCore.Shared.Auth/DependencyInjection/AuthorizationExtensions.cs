using Bazario.AspNetCore.Shared.Auth.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Auth.DependencyInjection
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options => options.ConfigureRoles());

            return services;
        }

        private static AuthorizationOptions ConfigureRoles(
            this AuthorizationOptions options)
        {
            foreach (var role in Enum.GetValues<Role>())
            {
                options.AddPolicy(role.ToString(), policy =>
                {
                    policy.RequireRole(role.ToString());
                });
            }

            return options;
        }
    }
}
