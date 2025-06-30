namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    public interface IMessagePublisher
    {
        Task PublishAsync<TMessage>(
            TMessage message,
            MessageBrokerExchangeType exchangeType = MessageBrokerExchangeType.Direct,
            CancellationToken cancellationToken = default)
            where TMessage : class;
    }
}
