namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    public interface IMessagePublisher
    {
        Task PublishAsync<TMessage>(
            TMessage message,
            CancellationToken cancellationToken = default)
            where TMessage : class;
    }
}
