using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Helpers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker
{
    internal sealed class MessagePublisher(
        IRabbitMqConnection rabbitMqConnection)
        : IMessagePublisher
    {
        public async Task PublishAsync<TMessage>(
            TMessage message,
            MessageBrokerExchangeType exchangeType = MessageBrokerExchangeType.Direct,
            CancellationToken cancellationToken = default)
            where TMessage : class
        {
            using var chanel = await rabbitMqConnection.Connection
                .CreateChannelAsync(cancellationToken: cancellationToken);

            var routingKey = KebabCaseFormater.ToKebabCase<TMessage>();
            await DeclareDestinationAsync(exchangeType, chanel, routingKey, cancellationToken);

            var serializedMessage = JsonSerializer.Serialize(
                message,
                GetJsonSerializerOptions());

            var body = Encoding.UTF8.GetBytes(serializedMessage);
            await BasicPublishAsync(exchangeType, chanel, routingKey, body, cancellationToken);
        }

        private static async Task DeclareDestinationAsync(
            MessageBrokerExchangeType exchangeType,
            IChannel chanel,
            string routingKey,
            CancellationToken cancellationToken)
        {
            switch (exchangeType)
            {
                case MessageBrokerExchangeType.Direct:
                    await HandleDirectExchangeAsync(chanel, routingKey, cancellationToken);
                    break;
                case MessageBrokerExchangeType.Fanout:
                    await HandleFanoutExchangeAsync(chanel, routingKey, cancellationToken);
                    break;
                default:
                    throw new NotSupportedException($"Exchange type {exchangeType} is not supported.");
            }
        }

        private static async Task HandleFanoutExchangeAsync(
            IChannel chanel,
            string routingKey,
            CancellationToken cancellationToken)
        {
            await chanel.ExchangeDeclareAsync(
                exchange: routingKey,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);
        }

        private static async Task HandleDirectExchangeAsync(
            IChannel chanel,
            string routingKey,
            CancellationToken cancellationToken)
        {
            await chanel.QueueDeclareAsync(
                queue: routingKey,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }

        private static async Task BasicPublishAsync(
            MessageBrokerExchangeType exchangeType,
            IChannel chanel,
            string routingKey,
            byte[] body,
            CancellationToken cancellationToken)
        {
            (string exchangeValue, string routingKeyValue) =
                exchangeType switch
                {
                    MessageBrokerExchangeType.Direct => (string.Empty, routingKey),
                    MessageBrokerExchangeType.Fanout => (routingKey, string.Empty),
                    _ => throw new NotSupportedException($"Exchange type {exchangeType} is not supported."),
                };

            await chanel.BasicPublishAsync(
                exchange: exchangeValue,
                routingKey: routingKeyValue,
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true },
                body: body,
                cancellationToken: cancellationToken);
        }
    }
}
