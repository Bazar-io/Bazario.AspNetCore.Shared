using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates
{
    public static class BirthDateErrors
    {
        public static readonly Error Empty =
            Error.Validation(
                code: "BirthDate.Empty",
                description: "Birth Date cannot be empty or default.");

        public static Error MaxAgeExceeded(int maxAge) =>
            Error.Validation(
                code: "BirthDate.MaxAgeExceeded",
                description: $"Age cannot be greater than {maxAge} years.");

        public static Error BelowMinAge(int minAge) =>
            Error.Validation(
                code: "BirthDate.BelowMinAge",
                description: $"Age cannot be less than {minAge} years.");
    }
}
