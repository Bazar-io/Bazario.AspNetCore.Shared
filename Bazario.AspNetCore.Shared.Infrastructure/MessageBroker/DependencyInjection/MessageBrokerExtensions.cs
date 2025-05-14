using Bazario.AspNetCore.Shared.Application.Abstractions.EventBus;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Bazario.AspNetCore.Shared.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.DependencyInjection
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddMessageBroker(
            this IServiceCollection services,
            Assembly assembly)
        {
            var messageBrokerSettings = services.BuildServiceProvider().GetOptions<MessageBrokerSettings>();

            services.ConfigureMassTransit(
                assembly,
                messageBrokerSettings);

            services.AddTransient<IEventBus, EventBus>();

            return services;
        }

        private static IServiceCollection ConfigureMassTransit(
            this IServiceCollection services,
            Assembly assembly,
            MessageBrokerSettings settings)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();

                busConfigurator.AddConsumers(assembly);

                busConfigurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(settings.Host), h =>
                    {
                        h.Username(settings.User);
                        h.Password(settings.Password);
                    });

                    if (settings.EnableRetryPolicy)
                    {
                        cfg.UseMessageRetry(
                            r => r.Interval(
                                settings.RetryCount,
                                TimeSpan.FromMilliseconds(settings.RetryIntervalMilliseconds)));
                    }

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
