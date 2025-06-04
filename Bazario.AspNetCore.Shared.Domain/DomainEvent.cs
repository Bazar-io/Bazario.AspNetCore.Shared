using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;

namespace Bazario.AspNetCore.Shared.Domain
{
    public abstract record DomainEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();

        protected DomainEvent() { }
    }
}
