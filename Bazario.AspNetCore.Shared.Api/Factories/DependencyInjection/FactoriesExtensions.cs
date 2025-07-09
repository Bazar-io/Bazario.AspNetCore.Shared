using Microsoft.Extensions.DependencyInjection;

namespace Bazario.AspNetCore.Shared.Api.Factories.DependencyInjection
{
    public static class FactoriesExtensions
    {
        /// <summary>
        /// Registers the <see cref="ProblemDetailsFactory"/> as a singleton service.
        /// </summary>
        /// <param name="services">Service collection to which the factory will be added.</param>
        public static IServiceCollection AddProblemDetailsFactory(
            this IServiceCollection services)
        {
            services.AddSingleton<ProblemDetailsFactory>();

            return services;
        }
    }
}
