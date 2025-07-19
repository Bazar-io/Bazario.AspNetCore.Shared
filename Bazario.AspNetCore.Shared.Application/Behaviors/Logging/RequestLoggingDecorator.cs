using Bazario.AspNetCore.Shared.Abstractions.Messaging;
using Bazario.AspNetCore.Shared.Results;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bazario.AspNetCore.Shared.Application.Behaviors.Logging
{
    internal sealed class RequestLoggingDecorator
    {
        internal sealed class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            ILogger<QueryHandler<TQuery, TResponse>> logger)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
            {
                string requestName = typeof(TQuery).Name;

                logger.LogInformation("Processing request: {RequestName}", requestName);

                var result = await innerHandler.Handle(query, cancellationToken);

                LogResult(logger, requestName, result);

                return result;
            }
        }

        internal sealed class CommandHandler<TCommand>(
            ICommandHandler<TCommand> innerHandler,
            ILogger<CommandHandler<TCommand>> logger)
            : ICommandHandler<TCommand>
            where TCommand : ICommand
        {
            public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
            {
                string requestName = typeof(TCommand).Name;

                logger.LogInformation("Processing request: {RequestName}", requestName);

                var result = await innerHandler.Handle(command, cancellationToken);

                LogResult(logger, requestName, result);

                return result;
            }
        }

        internal sealed class CommandHandler<TCommand, TResponse>(
            ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger)
            : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
            {
                string requestName = typeof(TCommand).Name;

                logger.LogInformation("Processing request: {RequestName}", requestName);

                var result = await innerHandler.Handle(command, cancellationToken);

                LogResult(logger, requestName, result);

                return result;
            }
        }

        internal static void LogResult<T>(ILogger<T> logger, string requestName, Result result)
        {
            if (result.IsSuccess)
            {
                logger.LogInformation("Completed request: {RequestName}", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed request: {RequestName} with error", requestName);
                }
            }
        }
    }
}
