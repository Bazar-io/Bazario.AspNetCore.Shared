using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames
{
    public static class LastNameErrors
    {
        public static readonly Error Empty =
            Error.Validation(
                code: "LastName.Empty",
                description: "Last Name cannot be empty.");

        public static readonly Error InvalidFormat =
            Error.Validation(
                code: "LastName.InvalidFormat",
                description: "Last Name can only contain letters and the following characters: [-,']");

        public static Error TooLong(int maxLength) =>
            Error.Validation(
                code: "LastName.TooLong",
                description: $"Last Name exceeds {maxLength} characters.");

        public static Error TooShort(int minLength) =>
            Error.Validation(
                code: "LastName.TooShort",
                description: $"Last Name length cannot be less than {minLength} characters.");
    }
}
