using Bazario.AspNetCore.Shared.Application.Abstractions.EventBus;
using MassTransit;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker
{
    public sealed class EventBus(
        IPublishEndpoint publishEndpoint) : IEventBus
    {
        public Task PublishAsync<T>(
            T message,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return publishEndpoint.Publish(message, cancellationToken);
        }
    }
}
