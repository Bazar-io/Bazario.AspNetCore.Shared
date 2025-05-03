using Bazario.AspNetCore.Shared.Results;
using System.Text.RegularExpressions;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.Emails
{
    public sealed class Email : ValueObject
    {
        public const int MinLength = 5;
        public const int MaxLength = 50;

        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string value)
        {
            var validationResult = ValidateEmailString(value);

            if (validationResult.IsFailure)
            {
                return Result.Failure<Email>(validationResult.Error);
            }

            return new Email(value!);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        private static Result ValidateEmailString(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return EmailErrors.Empty;
            }

            if (email.Length < MinLength)
            {
                return EmailErrors.TooShort(
                    minLength: MinLength);
            }

            if (email.Length > MaxLength)
            {
                return EmailErrors.TooLong(
                    maxLength: MaxLength);
            }

            var emailRegex = new Regex(
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);

            if (!emailRegex.IsMatch(email))
            {
                return EmailErrors.InvalidFormat;
            }

            return Result.Success();
        }
    }
}
