namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents the generic result of a validation operation that can either succeed or fail.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that the operation returns upon success.</typeparam>
    public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(
            value: default,
            isSuccess: false,
            error: IValidationResult.ValidationError)
        {
            Errors = errors;
        }

        /// <summary>
        /// Gets a collection of validation errors that occurred during the operation.
        /// </summary>
        public Error[] Errors { get; }

        /// <summary>
        /// Creates a <see cref="ValidationResult{TValue}"/> with the specified validation errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="Error"/> objects representing the validation errors.</param>
        /// <returns>A <see cref="ValidationResult{TValue}"/> containing the specified validation errors</returns>
        public static ValidationResult<TValue> WithErrors(Error[] errors) => new(errors);
    }
}
