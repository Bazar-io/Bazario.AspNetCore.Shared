using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Options
{
    public static partial class OptionsExtensions
    {
        /// <summary>
        /// Gets options of type <typeparamref name="TOptions"/> from the service provider.
        /// </summary>
        /// <typeparam name="TOptions">Options type that implements <see cref="IAppOptions"/>.</typeparam>
        /// <param name="serviceProvider">Service provider to resolve options from.</param>
        /// <returns>Options of type <typeparamref name="TOptions"/>.</returns>
        public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class, IAppOptions
        {
            return serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
        }
    }
}
