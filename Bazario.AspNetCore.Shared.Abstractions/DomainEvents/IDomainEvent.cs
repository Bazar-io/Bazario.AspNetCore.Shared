namespace Bazario.AspNetCore.Shared.Abstractions.DomainEvents
{
    /// <summary>
    /// Defines a contract for a domain event.
    /// </summary>
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}