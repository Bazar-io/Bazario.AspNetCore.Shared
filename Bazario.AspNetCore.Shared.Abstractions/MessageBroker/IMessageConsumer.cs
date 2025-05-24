namespace Bazario.AspNetCore.Shared.Abstractions.MessageBroker
{
    public interface IMessageConsumer<in TMessage>
        where TMessage : class
    {
        Task ConsumeAsync(
            TMessage message,
            CancellationToken cancellationToken = default);
    }
}
