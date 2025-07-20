using System.Diagnostics.CodeAnalysis;

namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents the result of an operation that can either succeed or fail,
    /// with a value of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that the operation returns upon success.</typeparam>
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{TValue}"/> class.
        /// </summary>
        /// <param name="value">The value that the operation returns upon success.</param>
        /// <param name="isSuccess">A value indicating whether the operation was successful.</param>
        /// <param name="error">The error that occurred during the operation.</param>
        protected internal Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when trying to access the value of a failure result.
        /// </exception>
        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can't be accessed.");

        /// <summary>
        /// Implicitly converts a <see cref="Result{TValue}"/> to a <see cref="Result"/>.
        /// </summary>
        /// <param name="value">The <see cref="Result{TValue}"/> to convert.</param>
        public static implicit operator Result<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }
}
