using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence
{
    public interface IHasOutboxMessages
    {
        DbSet<OutboxMessage> OutboxMessages { get; }
    }
}
