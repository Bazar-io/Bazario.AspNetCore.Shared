using Bazario.AspNetCore.Shared.Abstractions.MessageBroker;
using Bazario.AspNetCore.Shared.Infrastructure.Abstractions;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Helpers;
using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker
{
    internal sealed class MessageConsumerHostedService<TMessage>
        : BackgroundService
        where TMessage : class
    {
        private readonly MessageConsumerExchangeTypeSettings<TMessage> _exchangeTypeSettings;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<MessageConsumerHostedService<TMessage>> _logger;

        private IChannel? _channel;

        public MessageConsumerHostedService(
            MessageConsumerExchangeTypeSettings<TMessage> exchangeTypeSettings,
            IRabbitMqConnection rabbitMqConnection,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<MessageConsumerHostedService<TMessage>> logger)
        {
            _exchangeTypeSettings = exchangeTypeSettings;
            _rabbitMqConnection = rabbitMqConnection;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _channel = await _rabbitMqConnection.Connection
                .CreateChannelAsync(cancellationToken: stoppingToken);

            var routingKey = KebabCaseFormater.ToKebabCase<TMessage>();
            var uniqueQueueName = $"{routingKey}-{Guid.NewGuid()}";
            await DeclareDestinationAsync(_channel, routingKey, uniqueQueueName, stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (sender, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();

                string message = Encoding.UTF8.GetString(body);

                TMessage? deserializedMessage = null;

                try
                {
                    deserializedMessage = JsonSerializer.Deserialize<TMessage>(
                         message,
                         GetJsonSerializerOptions());
                }
                catch (JsonException ex)
                {
                    _logger.LogError("Failed to deserialize message: {Message}", ex.Message);
                }

                if (deserializedMessage is null)
                {
                    // Log the error and do not acknowledge the message

                    _logger.LogError("Failed to deserialize message: {Message} was null.", message);

                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicNackAsync(
                        eventArgs.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);

                    return;
                }

                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                var messageConsumer = scope.ServiceProvider
                    .GetRequiredService<IMessageConsumer<TMessage>>();

                await messageConsumer.ConsumeAsync(
                    message: deserializedMessage,
                    cancellationToken: stoppingToken);

                // Acknowledge the message to the broker

                await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(
                    eventArgs.DeliveryTag,
                    multiple: false,
                    cancellationToken: stoppingToken);
            };

            var queueName = _exchangeTypeSettings.ExchangeType switch
            {
                MessageBrokerExchangeType.Direct => routingKey,
                MessageBrokerExchangeType.Fanout => uniqueQueueName,
                _ => throw new NotSupportedException($"Exchange type {_exchangeTypeSettings.ExchangeType} is not supported.")
            };

            await _channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            // Keep the service running until stopped
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        private async Task DeclareDestinationAsync(
            IChannel channel,
            string routingKey,
            string uniqueQueueName,
            CancellationToken stoppingToken)
        {
            switch (_exchangeTypeSettings.ExchangeType)
            {
                case MessageBrokerExchangeType.Direct:
                    await HandleDirectExchangeAsync(channel, routingKey, stoppingToken);
                    break;
                case MessageBrokerExchangeType.Fanout:
                    await HandleFanoutExchangeAsync(channel, routingKey, uniqueQueueName, stoppingToken);
                    break;
                default:
                    _logger.LogError("Unsupported exchange type: {ExchangeType}", _exchangeTypeSettings.ExchangeType);
                    throw new NotSupportedException($"Exchange type {_exchangeTypeSettings.ExchangeType} is not supported.");
            }
        }

        private static async Task HandleDirectExchangeAsync(
            IChannel channel,
            string routingKey,
            CancellationToken stoppingToken)
        {
            await channel.QueueDeclareAsync(
                queue: routingKey,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken);
        }

        private static async Task HandleFanoutExchangeAsync(
            IChannel channel,
            string routingKey,
            string uniqueQueueName,
            CancellationToken stoppingToken)
        {
            await channel.ExchangeDeclareAsync(
                exchange: routingKey,
                durable: true,
                autoDelete: false,
                type: ExchangeType.Fanout,
                cancellationToken: stoppingToken);
            await channel.QueueDeclareAsync(
                queue: uniqueQueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken);
            await channel.QueueBindAsync(
                queue: uniqueQueueName,
                exchange: routingKey,
                routingKey: string.Empty,
                cancellationToken: stoppingToken);
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping message consumer for {MessageType}", typeof(TMessage).Name);

            if (_channel is not null)
            {
                try
                {
                    await _channel.CloseAsync(
                        cancellationToken: cancellationToken);
                    _channel.Dispose();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while stopping message consumer for {MessageType}", typeof(TMessage).Name);
                }
            }

            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            base.Dispose();
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
