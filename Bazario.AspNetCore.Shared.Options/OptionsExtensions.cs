using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Options
{
    public static partial class OptionsExtensions
    {
        public static TOptions GetOptions<TOptions>(this IServiceProvider serviceProvider)
            where TOptions : class, IAppOptions
        {
            return serviceProvider.GetRequiredService<IOptions<TOptions>>().Value;
        }
    }
}
