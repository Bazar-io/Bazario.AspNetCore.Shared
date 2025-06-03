using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection
{
    public static class ValidationPipelineBehaviorExtensions
    {
        public static IServiceCollection AddValidationPipelineBehavior(
            this IServiceCollection services)
        {
            services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationDecorator.QueryHandler<,>));
            services.Decorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandHandler<>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));

            return services;
        }
    }
}
