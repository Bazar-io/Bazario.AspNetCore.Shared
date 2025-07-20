namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents the result of an operation that can either succeed or fail.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        /// <param name="error">The error that occurred during the operation.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is invalid.
        /// </exception>
        protected internal Result(
            bool isSuccess,
            Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error that occurred during the operation.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Creates a successful result with no error.
        /// </summary>
        /// <returns>A <see cref="Result"/> indicating success with no error.</returns>
        public static Result Success() => new(true, Error.None);

        /// <summary>
        /// Creates a successful result with a value of type <typeparamref name="TValue"/>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value that the operation returns upon success.</typeparam>
        /// <param name="value">The value that the operation returns upon success.</param>
        /// <returns>A <see cref="Result{TValue}"/> indicating success with the specified value.</returns>
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        /// <summary>
        /// Creates a failed result with the specified error.
        /// </summary>
        /// <param name="error">The error that occurred during the operation.</param>
        /// <returns>A <see cref="Result"/> indicating failure with the specified error.</returns>
        public static Result Failure(Error error) => new(false, error);

        /// <summary>
        /// Creates a failed generic result with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The type of the value that the operation returns upon failure.</typeparam>
        /// <param name="error">The error that occurred during the operation.</param>
        /// <returns>A <see cref="Result{TValue}"/> indicating failure with the specified error.</returns>
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }
}
