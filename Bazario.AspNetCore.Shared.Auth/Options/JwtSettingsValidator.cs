using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Auth.Options
{
    [OptionsValidator]
    public partial class JwtSettingsValidator
        : IValidateOptions<JwtSettings>
    {
    }
}
