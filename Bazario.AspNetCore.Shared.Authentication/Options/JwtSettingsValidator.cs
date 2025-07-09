using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Authentication.Options
{
    /// <summary>
    /// Validator for <see cref="JwtSettings"/> options.
    /// </summary>
    [OptionsValidator]
    public partial class JwtSettingsValidator
        : IValidateOptions<JwtSettings>
    {
    }
}
