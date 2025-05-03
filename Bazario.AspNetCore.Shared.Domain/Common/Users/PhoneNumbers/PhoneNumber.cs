using Bazario.AspNetCore.Shared.Results;
using System.Text.RegularExpressions;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.PhoneNumbers
{
    public sealed class PhoneNumber : ValueObject
    {
        // Matches: +1234567890, (123) 456-7890, 123-456-7890, etc.
        public const string FormatValidationPattern = @"^\+?[0-9\s\-\(\)]{7,20}$";

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static bool IsInvalidFormat(string value) =>
            !Regex.IsMatch(value, FormatValidationPattern);

        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            var validationResult = ValidatePhoneNumber(phoneNumber);

            if (validationResult.IsFailure)
            {
                return Result.Failure<PhoneNumber>(validationResult.Error);
            }

            return new PhoneNumber(phoneNumber!);
        }

        private static Result ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return Result.Failure(PhoneNumberErrors.Empty);
            }

            if (IsInvalidFormat(phoneNumber))
            {
                return Result.Failure(PhoneNumberErrors.InvalidFormat);
            }

            return Result.Success();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
