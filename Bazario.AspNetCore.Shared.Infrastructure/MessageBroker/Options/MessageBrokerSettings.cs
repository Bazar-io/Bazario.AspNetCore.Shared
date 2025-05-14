using Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options.Attributes;
using Bazario.AspNetCore.Shared.Options;
using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Infrastructure.MessageBroker.Options
{
    public sealed class MessageBrokerSettings : IAppOptions
    {
        public const string SectionName = nameof(MessageBrokerSettings);

        [Required]
        [AmqpUrl]
        public required string Host { get; init; }

        [Required]
        public required string User { get; init; }

        [Required]
        public required string Password { get; init; }

        [Range(1, 100)]
        public int RetryCount { get; init; }

        [Range(100, 60000)]
        public int RetryIntervalMilliseconds { get; init; }

        [Required]
        public bool EnableRetryPolicy { get; init; }
    }
}
