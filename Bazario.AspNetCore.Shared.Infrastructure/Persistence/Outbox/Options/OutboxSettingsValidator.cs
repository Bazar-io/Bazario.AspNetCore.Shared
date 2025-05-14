using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox.Options
{
    [OptionsValidator]
    public partial class OutboxSettingsValidator
        : IValidateOptions<OutboxSettings>
    {
    }
}
