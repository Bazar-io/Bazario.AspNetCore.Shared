﻿using Bazario.AspNetCore.Shared.Options;
using System.ComponentModel.DataAnnotations;

namespace Bazario.AspNetCore.Shared.Authentication.Options
{
    /// <summary>
    /// Represents settings for JWT authentication.
    /// </summary>
    public class JwtSettings : IAppOptions
    {
        public const string SectionName = nameof(JwtSettings);

        [Required]
        public string Issuer { get; init; }

        [Required]
        public string Audience { get; init; }

        [Required]
        public string SecretKey { get; init; }

        [Required]
        public int ExpirationTimeInMinutes { get; init; }

        [Required]
        public string SecurityAlgorithm { get; init; }
    }
}
