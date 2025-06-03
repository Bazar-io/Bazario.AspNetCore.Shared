using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Bazario.AspNetCore.Shared.Application.Messaging.DependencyInjection
{
    public static class MessagingExtensions
    {
        public static IServiceCollection AddMessaging(
            this IServiceCollection services,
            Type assemblyType)
        {
            services.Scan(scan =>
                scan.FromAssembliesOf(assemblyType)
                    .AddHandlersOfType(typeof(IQueryHandler<,>))
                    .AddHandlersOfType(typeof(ICommand))
                    .AddHandlersOfType(typeof(ICommandHandler<,>)));

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
