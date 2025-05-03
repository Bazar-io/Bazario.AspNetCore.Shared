using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames
{
    public static class FirstNameErrors
    {
        public static readonly Error Empty =
            Error.Validation(
                code: "FirstName.Empty",
                description: "First Name cannot be empty.");

        public static readonly Error InvalidFormat =
            Error.Validation(
                code: "FirstName.InvalidFormat",
                description: "First Name can only contain letters and the following characters: [-,']");

        public static Error TooLong(int maxLength) =>
            Error.Validation(
                code: "FirstName.TooLong",
                description: $"First Name exceeds {maxLength} characters.");

        public static Error TooShort(int minLength) =>
            Error.Validation(
                code: "FirstName.TooShort",
                description: $"First Name length cannot be less than {minLength} characters.");
    }
}
