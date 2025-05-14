using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options.Attributes
{
    public sealed class AmqpUrlAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is string str && Uri.TryCreate(str, UriKind.Absolute, out var uri))
            {
                return uri.Scheme == "amqp" || uri.Scheme == "amqps";
            }

            return false;
        }
    }
}
