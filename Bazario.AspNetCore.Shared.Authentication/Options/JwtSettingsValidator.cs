using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Authentication.Options
{
    [OptionsValidator]
    public partial class JwtSettingsValidator
        : IValidateOptions<JwtSettings>
    {
    }
}
