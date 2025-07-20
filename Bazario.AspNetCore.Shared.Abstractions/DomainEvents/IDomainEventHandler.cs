namespace Bazario.AspNetCore.Shared.Abstractions.DomainEvents
{
    /// <summary>
    /// Defines a contract for a domain event handler.
    /// </summary>
    /// <typeparam name="TDomainEvent">
    /// The type of the domain event that this handler will process.
    /// This should be an instance of a class that implements <see cref="IDomainEvent"/>.
    /// </typeparam>
    public interface IDomainEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}