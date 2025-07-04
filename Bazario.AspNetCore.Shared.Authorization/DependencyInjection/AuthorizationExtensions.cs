using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Authorization.DependencyInjection
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.ConfigurePolicy(
                    permission: Permission.Authenticated,
                    roles: [Role.User, Role.Admin, Role.Owner]);

                options.ConfigurePolicy(
                    permission: Permission.User,
                    roles: [Role.User]);

                options.ConfigurePolicy(
                    permission: Permission.Admin,
                    roles: [Role.Admin]);

                options.ConfigurePolicy(
                    permission: Permission.Owner,
                    roles: [Role.Owner]);

                options.ConfigurePolicy(
                    permission: Permission.ChangeOwnData,
                    roles: [Role.User, Role.Admin]);

                options.ConfigurePolicy(
                    permission: Permission.ManageUsers,
                    roles: [Role.Admin, Role.Owner]);

                options.ConfigurePolicy(
                    permission: Permission.ManageAdmins,
                    roles: [Role.Owner]);

                options.ConfigurePolicy(
                    permission: Permission.ManageInternalResources,
                    roles: [Role.Admin, Role.Owner]);
            });

            return services;
        }

        private static AuthorizationOptions ConfigurePolicy(
            this AuthorizationOptions options,
            Permission permission,
            params Role[] roles)
        {
            options.AddPolicy(permission.ToString(),
                options =>
                {
                    options
                        .RequireAuthenticatedUser()
                        .RequireRole(roles.Select(x => x.ToString()));
                });

            return options;
        }
    }
}
