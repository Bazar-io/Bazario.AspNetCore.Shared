﻿namespace Bazario.AspNetCore.Shared.Results
{
    public enum ErrorType
    {
        InternalFailure = 0,
        OperationFailure = 1,
        Validation = 2,
        NotFound = 3,
        Unauthorized = 4
    }
}
