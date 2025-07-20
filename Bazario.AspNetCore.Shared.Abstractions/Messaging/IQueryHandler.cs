using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Abstractions.Messaging
{
    /// <summary>
    /// Defines a contract for a query handler that processes queries of type <typeparamref name="TQuery"/>.
    /// </summary>
    /// <typeparam name="TQuery">
    /// The type of the query that this handler will process. 
    /// Must implement <see cref="IQuery{TResponse}"/>.
    /// </typeparam>
    /// <typeparam name="TResponse">The type of the response that this handler will return.</typeparam>
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        /// <summary>
        /// Handles the specified query and returns a response.
        /// </summary>
        /// <param name="query">The query to handle.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A <see cref="Result{TResponse}"/> containing the response of the query handling operation.</returns>
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
