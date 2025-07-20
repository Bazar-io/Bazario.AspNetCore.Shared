namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Defines contract for the result of a validation operation.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Returns an <see cref="Error"/> representing a validation error.
        /// </summary>
        public static readonly Error ValidationError =
            Error.Validation(
                code: "ValidationError",
                description: "A validation problem occurred.");

        /// <summary>
        /// Gets a collection of validation errors that occurred during the operation.
        /// </summary>
        Error[] Errors { get; }
    }
}
