using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Helpers;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker
{
    internal sealed class MessagePublisher(IRabbitMqConnection rabbitMqConnection,
        ILogger<MessagePublisher> logger)
        : IMessagePublisher
    {
        public async Task PublishAsync<TMessage>(
            TMessage message,
            CancellationToken cancellationToken = default)
            where TMessage : class
        {
            using var chanel = await rabbitMqConnection.Connection
                .CreateChannelAsync(cancellationToken: cancellationToken);

            var queueName = KebabCaseFormater.ToKebabCase<TMessage>();
            
            await chanel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: cancellationToken);

            var serializedMessage = JsonSerializer.Serialize(
                message,
                GetJsonSerializerOptions());

            var body = Encoding.UTF8.GetBytes(serializedMessage);

            await chanel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true },
                body: body,
                cancellationToken: cancellationToken);
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
        }
    }
}
