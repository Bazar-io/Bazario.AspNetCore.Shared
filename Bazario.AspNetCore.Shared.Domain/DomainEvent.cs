using MediatR;

namespace Bazario.AspNetCore.Shared.Domain
{
    public abstract record DomainEvent : INotification
    {
        public Guid Id { get; } = Guid.NewGuid();

        protected DomainEvent() { }
    }
}
