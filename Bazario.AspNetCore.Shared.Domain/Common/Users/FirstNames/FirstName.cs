using Bazario.AspNetCore.Shared.Results;
using System.Text.RegularExpressions;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames
{
    public sealed class FirstName : ValueObject
    {
        public const int MinLength = 2;
        public const int MaxLength = 30;

        private FirstName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<FirstName> Create(string? value)
        {
            var validationResult = ValidateInputValue(value);

            if (validationResult.IsFailure)
            {
                return Result.Failure<FirstName>(validationResult.Error);
            }

            return new FirstName(value!);
        }

        private static Result ValidateInputValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure(FirstNameErrors.Empty);
            }

            if (value.Length < MinLength)
            {
                return Result.Failure(
                    FirstNameErrors.TooShort(minLength: MinLength));
            }

            if (value.Length > MaxLength)
            {
                return Result.Failure(
                    FirstNameErrors.TooLong(maxLength: MaxLength));
            }

            var formatResult = ValidateFormat(value);

            if (formatResult.IsFailure)
            {
                return formatResult.Error;
            }

            return Result.Success();
        }

        private static Result ValidateFormat(string value)
        {
            var pattern = @"^[\p{L}\-,'\s]+$";

            if (!Regex.IsMatch(value, pattern))
            {
                return Result.Failure(FirstNameErrors.InvalidFormat);
            }

            return Result.Success();
        }
    }
}
