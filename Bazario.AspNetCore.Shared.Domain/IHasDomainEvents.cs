namespace Bazario.AspNetCore.Shared.Domain
{
    public interface IHasDomainEvents
    {
        IReadOnlyCollection<DomainEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}
