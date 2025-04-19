using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Infrastructure.MediatR.DependencyInjection
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRServices(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            return services;
        }
    }
}
