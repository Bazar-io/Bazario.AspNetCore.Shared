namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents an error that can occur during operations in the application.
    /// </summary>
    public record Error
    {
        /// <summary>
        /// Creates an instance of <see cref="Error"/> with no specific code or description.
        /// </summary>
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.OperationFailure);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> indicating that null value was provided. 
        /// </summary>
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", ErrorType.OperationFailure);

        private Error(
            string code,
            string description,
            ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        /// <summary>
        /// Gets the error code that uniquely identifies the error.
        /// </summary>
        public string Code { get; init; }

        /// <summary>
        /// Gets the error description that provides additional context about the error.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the type of the error, which can be used to categorize the error.
        /// </summary>
        public ErrorType ErrorType { get; init; }

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing an internal failure.
        /// </summary>
        /// <param name="code">The error code that uniquely identifies the internal failure.</param>
        /// <param name="description">The error description that provides additional context about the internal failure.</param>
        /// <returns>An instance of <see cref="Error"/> representing an internal failure.</returns>
        public static Error InternalFailure(string code, string description) =>
            new(code, description, ErrorType.InternalFailure);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing an internal failure with default values.
        /// </summary>
        /// <returns>An instance of <see cref="Error"/> representing an internal failure with default values.</returns>
        public static Error InternalFailure() =>
            new(string.Empty, string.Empty, ErrorType.InternalFailure);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing an operation failure.
        /// </summary>
        /// <param name="code">The error code that uniquely identifies the operation failure.</param>
        /// <param name="description">The error description that provides additional context about the operation failure.</param>
        /// <returns>An instance of <see cref="Error"/> representing an operation failure.</returns>
        public static Error OperationFailure(string code, string description) =>
            new(code, description, ErrorType.OperationFailure);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing a not found error.
        /// </summary>
        /// <param name="code">The error code that uniquely identifies the not found error.</param>
        /// <param name="description">The error description that provides additional context about the not found resource.</param>
        /// <returns>An instance of <see cref="Error"/> representing a not found error.</returns>
        public static Error NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing a validation error.
        /// </summary>
        /// <param name="code">The error code that uniquely identifies the validation error.</param>
        /// <param name="description">The error description that provides additional context about the validation error.</param>
        /// <returns>An instance of <see cref="Error"/> representing a validation error.</returns>
        public static Error Validation(string code, string description) =>
            new(code, description, ErrorType.Validation);

        /// <summary>
        /// Creates an instance of <see cref="Error"/> representing an unauthorized access error.
        /// </summary>
        /// <param name="code">The error code that uniquely identifies the unauthorized access error.</param>
        /// <param name="description">The error description that provides additional context about the unauthorized access error.</param>
        /// <returns>An instance of <see cref="Error"/> representing an unauthorized access error.</returns>
        public static Error Unauthorized(string code, string description) =>
           new(code, description, ErrorType.Unauthorized);

        /// <summary>
        /// Implicitly converts an <see cref="Error"/> to a <see cref="Result"/> indicating failure.
        /// </summary>
        /// <param name="error">The <see cref="Error"/> to convert.</param>
        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}
