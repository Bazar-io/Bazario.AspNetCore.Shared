namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    /// <summary>
    /// Defines a contract for a message consumer.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message that this consumer will process.</typeparam>
    public interface IMessageConsumer<in TMessage>
        where TMessage : class
    {
        /// <summary>
        /// Asynchronously consumes a message of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <param name="message">The message to consume. 
        /// This should be an instance of a class that implements <typeparamref name="TMessage"/>.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task ConsumeAsync(
            TMessage message,
            CancellationToken cancellationToken = default);
    }
}
