using Microsoft.Extensions.Options;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options
{
    [OptionsValidator]
    public partial class MessageBrokerSettingsValidator
        : IValidateOptions<MessageBrokerSettings>
    {
    }
}
