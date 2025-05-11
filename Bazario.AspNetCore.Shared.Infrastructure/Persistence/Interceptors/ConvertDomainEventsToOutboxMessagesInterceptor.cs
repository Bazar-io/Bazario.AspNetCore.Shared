using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor
        : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entitiesWithDomainEvents = FindAllEntitiesWithDomainEvents(
                dbContext);

            var domainEvents = SelectDomainEvents(entitiesWithDomainEvents);

            ClearDomainEvents(entitiesWithDomainEvents);

            var outboxMessages = ConvertDomainEventsToOutboxMessages(domainEvents);

            await dbContext.Set<OutboxMessage>().AddRangeAsync(outboxMessages);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static List<IHasDomainEvents> FindAllEntitiesWithDomainEvents(
           DbContext dbContext)
        {
            return [.. dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entry => entry.Entity.DomainEvents.Count != 0)
                .Select(entry => entry.Entity)];
        }

        private static List<DomainEvent> SelectDomainEvents(
            List<IHasDomainEvents> entitiesWithDomainEvents)
        {
            return [.. entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents)];
        }

        private static void ClearDomainEvents(
            List<IHasDomainEvents> entitiesWithDomainEvents)
        {
            foreach (var entity in entitiesWithDomainEvents)
            {
                entity.ClearDomainEvents();
            }
        }

        private static List<OutboxMessage> ConvertDomainEventsToOutboxMessages(
            List<DomainEvent> domainEvents)
        {
            return [.. domainEvents
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().Name,
                    Content = SerializeDomainEventContent(domainEvent),
                    OccurredOnUtc = DateTime.UtcNow
                })];
        }

        private static string SerializeDomainEventContent(
            DomainEvent domainEvent)
        {
            return JsonConvert.SerializeObject(
                domainEvent,
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                });
        }
    }
}
