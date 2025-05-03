using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers
{
    public static class PhoneNumberErrors
    {
        public static readonly Error Empty =
            Error.Validation(
                code: "PhoneNumber.Empty",
                description: "Phone Number cannot be empty.");

        public static readonly Error InvalidFormat =
            Error.Validation(
                code: "PhoneNumber.InvalidFormat",
                description: "Phone Number is in invalid format.");
    }
}
