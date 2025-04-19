using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Infrastructure.Options.DependencyInjection
{
    public static partial class OptionsExtensions
    {
        public static IServiceCollection ConfigureValidatableOptions<TOptions, TOptionsValidator>(
            this IServiceCollection services,
            string configurationSectionPath)
            where TOptions : class, IAppOptions
            where TOptionsValidator : class, IValidateOptions<TOptions>
        {
            services.AddOptions<TOptions>()
                .BindConfiguration(configurationSectionPath);

            services.AddSingleton<IValidateOptions<TOptions>, TOptionsValidator>();

            return services;
        }

        public static IServiceProvider ValidateAppOptionsOnStart(
            this IServiceProvider serviceProvider,
            Assembly assembly)
        {
            var optionsTypes = assembly
                .GetTypes()
                .Where(type => typeof(IAppOptions).IsAssignableFrom(type) && !type.IsAbstract)
                .ToList();

            foreach (var optionsType in optionsTypes)
            {
                var validateMethod = typeof(OptionsExtensions)
                    .GetMethod(nameof(ValidateOptionsOnStart))
                    ?.MakeGenericMethod(optionsType);

                validateMethod?.Invoke(null, [serviceProvider]);
            }

            return serviceProvider;
        }

        public static IServiceProvider ValidateOptionsOnStart<TOptions>(
            this IServiceProvider serviceProvider)
            where TOptions : class, IAppOptions
        {
            var options = serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;

            if (options is null)
            {
                throw new InvalidOperationException(
                    $"Options of type {typeof(TOptions).Name} cannot be null.");
            }

            var validator = serviceProvider.GetRequiredService<IValidateOptions<TOptions>>();

            var validationResult = validator.Validate(
                null,
                options);

            if (validationResult.Failed)
            {
                throw new InvalidOperationException(
                    $"Options validation failed: {validationResult.FailureMessage}");
            }

            return serviceProvider;
        }
    }
}
