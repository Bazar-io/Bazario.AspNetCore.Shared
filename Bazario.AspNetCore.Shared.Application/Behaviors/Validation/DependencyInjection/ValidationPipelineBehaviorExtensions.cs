using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection
{
    public static class ValidationPipelineBehaviorExtensions
    {
        public static IServiceCollection AddValidationPipelineBehavior(
            this IServiceCollection services)
        {
            services.TryDecorate(typeof(IQueryHandler<,>), typeof(ValidationDecorator.QueryHandler<,>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));

            return services;
        }
    }
}
