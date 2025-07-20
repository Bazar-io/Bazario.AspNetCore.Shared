namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    /// <summary>
    /// Defines a contract for a message publisher.
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        /// Asynchronously publishes a message of type <typeparamref name="TMessage"/> to the message broker.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message that this publisher will publish.</typeparam>
        /// <param name="message">The message to publish.</param>
        /// <param name="exchangeType">The type of exchange to use when publishing the message.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task PublishAsync<TMessage>(
            TMessage message,
            MessageBrokerExchangeType exchangeType = MessageBrokerExchangeType.Direct,
            CancellationToken cancellationToken = default)
            where TMessage : class;
    }
}
