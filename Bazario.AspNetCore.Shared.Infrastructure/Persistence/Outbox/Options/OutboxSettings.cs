using Bazario.AspNetCore.Shared.Options;
using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.Outbox.Options
{
    public sealed class OutboxSettings : IAppOptions
    {
        public const string SectionName = nameof(OutboxSettings);

        [Required]
        [Range(1, int.MaxValue)]
        public int BatchSize { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ProcessIntervalInSeconds { get; init; }
    }
}
