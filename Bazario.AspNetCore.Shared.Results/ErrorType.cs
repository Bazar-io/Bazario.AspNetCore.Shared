namespace Bazario.AspNetCore.Shared.Results
{
    /// <summary>
    /// Represents the type of error that can occur during operations in the application.
    /// </summary>
    public enum ErrorType
    {
        InternalFailure = 0,
        OperationFailure = 1,
        Validation = 2,
        NotFound = 3,
        Unauthorized = 4
    }
}
