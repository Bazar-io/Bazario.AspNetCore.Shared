namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents the result of a validation operation that can either succeed or fail.
    /// </summary>
    public sealed class ValidationResult : Result, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(
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
        /// Creates a <see cref="ValidationResult"/> with the specified validation errors.
        /// </summary>
        /// <param name="errors">An array of <see cref="Error"/> objects representing the validation errors.</param>
        /// <returns>A <see cref="ValidationResult"/> containing the specified validation errors.</returns>
        public static ValidationResult WithErrors(Error[] errors) => new(errors);
    }
}
