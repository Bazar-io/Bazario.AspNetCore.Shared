using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;

namespace Bazario.AspNetCore.Shared.Infrastructure.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(
            IDomainEvent domainEvent,
            CancellationToken cancellationToken = default);
    }
}