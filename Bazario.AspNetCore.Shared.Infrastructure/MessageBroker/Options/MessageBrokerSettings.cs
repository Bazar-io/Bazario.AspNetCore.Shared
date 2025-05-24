using Bazario.AspNetCore.Shared.Options;
using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options
{
    public sealed class MessageBrokerSettings : IAppOptions
    {
        public const string SectionName = nameof(MessageBrokerSettings);

        [Required]
        public required string Host { get; init; }

        [Required]
        public required int Port { get; init; }

        [Required]
        public required string Username { get; init; }

        [Required]
        public required string Password { get; init; }
    }
}
