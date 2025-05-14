using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options
{
    [OptionsValidator]
    public partial class DbSettingsValidator
        : IValidateOptions<DbSettings>
    {
    }
}
