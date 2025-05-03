using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Domain.Common.Users.BirthDates
{
    public sealed class BirthDate : ValueObject
    {
        public const int MinAge = 18;
        public const int MaxAge = 120;

        private BirthDate(DateOnly value)
        {
            Value = value;
        }

        public DateOnly Value { get; }

        public static Result<BirthDate> Create(DateOnly date)
        {
            if (date == DateOnly.MinValue)
            {
                return Result.Failure<BirthDate>(BirthDateErrors.Empty);
            }

            var today = DateOnly.FromDateTime(DateTime.Today);

            var earliestAllowed = today.AddYears(-MaxAge);
            var latestAllowed = today.AddYears(-MinAge);

            if (date < earliestAllowed)
            {
                return Result.Failure<BirthDate>(
                    BirthDateErrors.MaxAgeExceeded(maxAge: MaxAge));
            }

            if (date > latestAllowed)
            {
                return Result.Failure<BirthDate>(
                    BirthDateErrors.BelowMinAge(minAge: MinAge));
            }

            return Result.Success(new BirthDate(date));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
