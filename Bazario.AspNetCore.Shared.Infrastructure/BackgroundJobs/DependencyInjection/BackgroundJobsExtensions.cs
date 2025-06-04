using Bazario.AspNetCore.Shared.Infrastructure.DomainEvents;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence;
using Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox.Options;
using Bazario.AspNetCore.Shared.Options;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Bazario.AspNetCore.Shared.Infrastructure.BackgroundJobs.DependencyInjection
{
    public static class BackgroundJobsExtensions
    {
        public static IServiceCollection AddBackgroundJobs(
            this IServiceCollection services,
            params Action<IServiceCollection, IServiceCollectionQuartzConfigurator>[] jobConfigurations)
        {
            services.AddQuartz(configurator =>
            {
                var scheduler = Guid.NewGuid();

                configurator.SchedulerId = $"default-id-{scheduler}";
                configurator.SchedulerName = $"default-name-{scheduler}";

                foreach (var jobConfiguration in jobConfigurations)
                {
                    jobConfiguration.Invoke(services, configurator);
                }
            });

            services.AddQuartzHostedService();

            return services;
        }

        public static void ConfigureProcessOutboxMessagesJob<DbContext>(
            IServiceCollection services,
            IServiceCollectionQuartzConfigurator configurator)
            where DbContext : Microsoft.EntityFrameworkCore.DbContext, IHasOutboxMessages
        {
            var outboxSettings = services.BuildServiceProvider().GetOptions<OutboxSettings>();

            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob<DbContext>));

            configurator
                .AddJob<ProcessOutboxMessagesJob<DbContext>>(jobKey)
                .AddTrigger(
                    trigger => trigger.ForJob(jobKey)
                        .WithSimpleSchedule(
                            schedule => schedule
                                .WithIntervalInSeconds(
                                    outboxSettings.ProcessIntervalInSeconds)
                                .RepeatForever()));

            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        }
    }
}
