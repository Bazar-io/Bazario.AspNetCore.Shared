using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Application.DomainEvents.DependencyInjection
{
    public static class DomainEventsExtensions
    {
        public static IServiceCollection AddDomainEventHandlers(
            this IServiceCollection services,
            Assembly assembly)
        {
            services.Scan(scan =>
                scan.FromAssemblies(assembly)
                    .AddHandlersOfType(typeof(IDomainEventHandler<>)));

            return services;
        }

        private static IImplementationTypeSelector AddHandlersOfType(
            this IImplementationTypeSelector selector,
            Type assignableType)
        {
            return selector.AddClasses(classes => classes.AssignableTo(assignableType), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        }
    }
}
