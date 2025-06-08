using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Api.Factories.DependencyInjection
{
    public static class FactoriesExtensions
    {
        public static IServiceCollection AddProblemDetailsFactory(
            this IServiceCollection services)
        {
            services.AddSingleton<ProblemDetailsFactory>();

            return services;
        }
    }
}
