using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Bazario.AspNetCore.Shared.Options.DependencyInjection
{
    public static partial class OptionsExtensions
    {
        /// <summary>
        /// Registers and configures options of type TOptions with a configuration section path.
        /// </summary>
        /// <remarks>
        /// Validator of type TOptionsValidator has to be partial and have the <see cref="OptionsValidatorAttribute"/> attribute
        /// </remarks>
        /// <typeparam name="TOptions">Options type that implements <see cref="IAppOptions"/>.</typeparam>
        /// <typeparam name="TOptionsValidator">Validator type that implements <see cref="IValidateOptions{TOptions}"/>.</typeparam>
        /// <param name="services">Service collection to register the options and validator.</param>
        /// <param name="configurationSectionPath">Path to the configuration section in the 
        /// appsettings.json or other configuration sources.</param>
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

        /// <summary>
        /// Validates all options implementing <see cref="IAppOptions"/> on application start.
        /// Uses reflection to find all such options types in the specified assembly
        /// and executes <see cref="ValidateOptionsOnStart{TOptions}"/> for each of them.
        /// </summary>
        /// <param name="serviceProvider">Service provider to resolve options and validators.</param>
        /// <param name="assembly">Assembly to search for options types implementing <see cref="IAppOptions"/>.</param>
        /// <exception cref="InvalidOperationException">If any options type implementing <see cref="IAppOptions"/> cannot
        /// be resolved or validation fails.</exception>
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

        /// <summary>
        /// Validates options of type TOptions on application start.
        /// </summary>
        /// <typeparam name="TOptions">Options type that implements <see cref="IAppOptions"/>.</typeparam>
        /// <param name="serviceProvider">Service provider to resolve options and validator.</param>
        /// <exception cref="InvalidOperationException">If <typeparamref name="TOptions"/> 
        /// aren't resolved or validation fails.</exception>
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
