using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection
{
    public static class ValidationPipelineBehaviorExtensions
    {
        public static IServiceCollection AddValidationPipelineBehavior(
            this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }
    }
}
