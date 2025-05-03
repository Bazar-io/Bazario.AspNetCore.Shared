using Bazario.AspNetCore.Shared.Domain.Common.Users.FirstNames;
using Bazario.AspNetCore.Shared.Results;
using System.Text.RegularExpressions;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.LastNames
{
    public sealed class LastName : ValueObject
    {
        public const int MinLength = 2;
        public const int MaxLength = 30;

        private LastName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static Result<LastName> Create(string value)
        {
            var validationResult = ValidateInputValue(value);

            if (validationResult.IsFailure)
            {
                return Result.Failure<LastName>(validationResult.Error);
            }

            return new LastName(value!);
        }

        private static Result ValidateInputValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result.Failure(FirstNameErrors.Empty);
            }

            if (value.Length < MinLength)
            {
                return Result.Failure(
                    LastNameErrors.TooShort(minLength: MinLength));
            }

            if (value.Length > MaxLength)
            {
                return Result.Failure(
                    LastNameErrors.TooLong(maxLength: MaxLength));
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
                return Result.Failure(LastNameErrors.InvalidFormat);
            }

            return Result.Success();
        }
    }
}
