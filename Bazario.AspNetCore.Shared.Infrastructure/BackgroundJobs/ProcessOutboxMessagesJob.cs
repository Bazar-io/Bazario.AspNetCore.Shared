using Bazario.AspNetCore.Shared.Abstractions.DomainEvents;
using Bazario.AspNetCore.Shared.Domain;
using Bazario.AspNetCore.Shared.Infrastructure.DomainEvents;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace Bazario.AspNetCore.Shared.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    internal sealed class ProcessOutboxMessagesJob<DbContext> : IJob
        where DbContext : Microsoft.EntityFrameworkCore.DbContext, IHasOutboxMessages
    {
        private readonly DbContext _context;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly OutboxSettings _settings;
        private readonly ILogger<ProcessOutboxMessagesJob<DbContext>> _logger;

        public ProcessOutboxMessagesJob(
            DbContext context,
            IDomainEventDispatcher domainEventDispatcher,
            IOptions<OutboxSettings> settings,
            ILogger<ProcessOutboxMessagesJob<DbContext>> logger)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
            _settings = settings.Value;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var messages = await GetOutboxMessagesAsync(
                    context.CancellationToken);
                _logger.LogDebug("Retrieved {MessageCount} outbox messages to process.", messages.Count);

                await ProcessOutboxMessages(
                    messages, context.CancellationToken);

                await _context.SaveChangesAsync();
                _logger.LogDebug("Successfully saved changes to the database.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database update error occurred while saving changes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex, "Unhandled exception occurred in job execution.");
                throw;
            }
        }

        private Task<List<OutboxMessage>> GetOutboxMessagesAsync(
            CancellationToken cancellationToken)
        {
            return _context.OutboxMessages
                .Where(message => message.ProcessedOnUtc == null)
                .Take(_settings.BatchSize)
                .ToListAsync(cancellationToken);
        }

        private async Task ProcessOutboxMessages(
            List<OutboxMessage> messages,
            CancellationToken cancellationToken)
        {
            if (messages.Count == 0)
            {
                _logger.LogDebug("No messages to process.");

                return;
            }

            var publishTasks = new List<Task>();

            foreach (var message in messages)
            {
                IDomainEvent? domainEvent = DeserializeDomainEvent(message.Content);
                if (domainEvent is null)
                {
                    _logger.LogError("Deserialization failed for message ID {MessageId}.", message.Id);
                    continue;
                }

                _logger.LogInformation("Publishing message ID {MessageId} of type {EventType}.", message.Id, domainEvent.GetType().Name);

                var publishTask = PublishDomainEvent(
                    message, domainEvent, cancellationToken);

                publishTasks.Add(publishTask);
            }

            await Task.WhenAll(publishTasks);
            _logger.LogDebug("Completed processing of {MessageCount} outbox messages.", messages.Count);
        }

        private static DomainEvent? DeserializeDomainEvent(string content)
        {
            return JsonConvert
                .DeserializeObject<DomainEvent>(
                    content,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });
        }

        private Task PublishDomainEvent(
            OutboxMessage message,
            IDomainEvent domainEvent,
            CancellationToken cancellationToken)
        {
            return _domainEventDispatcher.DispatchAsync(
                domainEvent,
                cancellationToken)
                .ContinueWith(task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        message.ProcessedOnUtc = DateTime.UtcNow;

                        _logger.LogInformation("Message ID {MessageId} successfully processed.", message.Id);
                    }
                    else if (task.IsFaulted)
                    {
                        _logger.LogError(task.Exception, "Error processing message ID {MessageId}.", message.Id);
                    }
                }, cancellationToken);
        }
    }
}