namespace Bazario.AspNetCore.Shared.Abstractions.DomainEvents
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}