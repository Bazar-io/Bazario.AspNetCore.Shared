namespace Bazario.AspNetCore.Shared.Abstractions.Messaging
{
    /// <summary>
    /// Defines a contract for a command that can be executed within the application.
    /// </summary>
    public interface ICommand : IBaseCommand;

    /// <summary>
    /// Defines a contract for a command that can be executed within the application and returns a response.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response that this command will return. </typeparam>
    public interface ICommand<TResponse> : IBaseCommand;

    /// <summary>
    /// Defines a base contract for commands that can be executed within the application.
    /// </summary>
    public interface IBaseCommand;
}
