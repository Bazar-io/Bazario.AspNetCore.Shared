using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Bazario.AspNetCore.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.DependencyInjection
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddMessageBroker(
            this IServiceCollection services)
        {
            var messageBrokerSettings = services.BuildServiceProvider().GetOptions<MessageBrokerSettings>();

            services.AddConnectionFactory(messageBrokerSettings);

            services.ConfigureRabbitMqConnection();

            services.ConfigureMessagePublisher();

            return services;
        }

        public static IServiceCollection AddMessageConsumer<TMessage, TMessageConsumer>(
            this IServiceCollection services,
            MessageBrokerExchangeType exchangeType = MessageBrokerExchangeType.Direct)
            where TMessage : class
            where TMessageConsumer : class, IMessageConsumer<TMessage>
        {
            // Register the message consumer exchange type settings
            services.AddSingleton(sp => new MessageConsumerExchangeTypeSettings<TMessage>(exchangeType));

            services.AddScoped<IMessageConsumer<TMessage>, TMessageConsumer>();
            services.AddHostedService<MessageConsumerHostedService<TMessage>>();

            return services;
        }

        private static IServiceCollection AddConnectionFactory(
            this IServiceCollection services,
            MessageBrokerSettings settings)
        {
            services.AddSingleton<IConnectionFactory>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = settings.Host,
                    Port = settings.Port,
                    UserName = settings.Username,
                    Password = settings.Password,
                };

                return factory;
            });

            return services;
        }

        private static IServiceCollection ConfigureRabbitMqConnection(
            this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection>(sp =>
            {
                var factory = sp.GetRequiredService<IConnectionFactory>();

                var connection = new RabbitMqConnection(factory);

                connection.InitializeAsync().GetAwaiter().GetResult();

                return connection;
            });

            return services;
        }

        private static IServiceCollection ConfigureMessagePublisher(
            this IServiceCollection services)
        {
            services.AddSingleton<IMessagePublisher, MessagePublisher>();

            return services;
        }
    }
}
