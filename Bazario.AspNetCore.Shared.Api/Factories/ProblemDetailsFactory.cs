﻿using Bazario.AspNetCore.Shared.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bazario.AspNetCore.Shared.Api.Factories
{
    /// <summary>
    /// Factory for creating <see cref="ProblemDetails"/> based on the provided <see cref="Result"/>.
    /// </summary>
    /// <remarks>Requires <see cref="IHttpContextAccessor" /> provided in service collection 
    /// to access the current HTTP context and activity features.</remarks>
    public class ProblemDetailsFactory
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ProblemDetailsFactory(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// Creates a <see cref="ProblemDetails"/> object based on the provided <see cref="Result"/>.
        /// </summary>
        /// <param name="result">Result containing error information to be converted into a <see cref="ProblemDetails"/> object.</param>
        /// <returns>
        /// <see cref="IActionResult"/> containing the <see cref="ProblemDetails"/> object if the result is not successful."/>
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the result is successful, indicating that 
        /// a problem details response should not be created.</exception>
        public IActionResult GetProblemDetails(Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }

            Activity? activity = _contextAccessor.HttpContext?.Features.Get<IHttpActivityFeature>()?.Activity;

            var error = result.Error;

            var problemDetails = new ProblemDetails
            {
                Type = GetType(error.ErrorType),
                Title = GetTitle(error.ErrorType),
                Detail = error.Description,
                Status = GetStatusCode(error.ErrorType),
                Instance = $"{_contextAccessor.HttpContext?.Request.Method} {_contextAccessor.HttpContext?.Request.Path}",
                Extensions = new Dictionary<string, object?>
                {
                    { "requestId", _contextAccessor.HttpContext?.TraceIdentifier },
                    { "traceId", activity?.Id }
                }
            };

            if (error.ErrorType != ErrorType.InternalFailure)
            {
                problemDetails.Extensions.Add("errors", new { code = error.Code, description = error.Description });

                if (result is IValidationResult validationResult)
                {
                    problemDetails.Extensions["errors"] = validationResult.Errors
                        .Select(e => new { e.Code, e.Description })
                        .ToArray();
                }
            }

            return new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status
            };
        }

        private static int GetStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.OperationFailure => StatusCodes.Status400BadRequest,
                ErrorType.Unauthorized => StatusCodes.Status403Forbidden,

                _ => StatusCodes.Status500InternalServerError
            };
        }

        private static string GetTitle(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.OperationFailure => "Bad Request",
                ErrorType.Unauthorized => "Unauthorized",

                _ => "Internal Server Error"
            };
        }

        private static string GetType(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.OperationFailure => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",

                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        }
    }
}
