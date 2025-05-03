using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Validation.DependencyInjection
{
    public static class ValidatorsExtensions
    {
        public static IServiceCollection AddValidators(
            this IServiceCollection services,
            Assembly assembly, 
            bool includeInternalTypes = true)
        {
            services.AddValidatorsFromAssembly(
                assembly,
                includeInternalTypes: includeInternalTypes);

            return services;
        }
    }
}
