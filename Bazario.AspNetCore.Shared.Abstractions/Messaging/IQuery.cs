namespace Bazario.AspNetCore.Shared.Abstractions.Messaging
{
    /// <summary>
    /// Defines a contract for a query that can be executed within the application.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response that this query will return.</typeparam>
    public interface IQuery<TResponse>;
}
