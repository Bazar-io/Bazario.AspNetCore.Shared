using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Logging.DependencyInjection
{
    public static class RequestLoggingPipelineBehaviorExtensions
    {
        public static IServiceCollection AddRequestLoggingPipelineBehavior(
            this IServiceCollection services)
        {
            services.TryDecorate(typeof(IQueryHandler<,>), typeof(RequestLoggingDecorator.QueryHandler<,>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(RequestLoggingDecorator.CommandHandler<>));
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(RequestLoggingDecorator.CommandHandler<,>));

            return services;
        }
    }
}
