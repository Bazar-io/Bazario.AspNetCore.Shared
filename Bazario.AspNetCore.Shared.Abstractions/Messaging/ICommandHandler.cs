using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Abstractions.Messaging
{
    /// <summary>
    /// Defines a contract for a command handler that processes commands of type <typeparamref name="TCommand"/>.
    /// </summary>
    /// <typeparam name="TCommand">
    /// The type of the command that this handler will process. 
    /// Must implement <see cref="ICommand"/>.
    /// </typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Result"/> indicating the outcome of the command handling operation.</returns>
        Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines a contract for a command handler that processes commands of type <typeparamref name="TCommand"/> 
    /// and returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <typeparam name="TCommand">
    /// The type of the command that this handler will process. 
    /// Must implement <see cref="ICommand{TResponse}"/>.
    /// </typeparam>
    /// <typeparam name="TResponse">The type of the response that this handler will return.</typeparam>
    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        /// <summary>
        /// Handles the specified command and returns a response.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Result{TResponse}"/> containing the response of the command handling operation.</returns>
        Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
    }
}
