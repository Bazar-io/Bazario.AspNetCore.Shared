using Bazario.AspNetCore.Shared.Options;
using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Infrastructure.Persistence.Options
{
    public class DbSettings : IAppOptions
    {
        public const string SectionName = nameof(DbSettings);

        [Required]
        public required string ConnectionString { get; init; }
    }
}
