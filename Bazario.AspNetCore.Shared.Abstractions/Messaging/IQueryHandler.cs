using Bazario.AspNetCore.Shared.Results;

namespace Bazario.AspNetCore.Shared.Abstractions.Messaging
{
    public interface IQueryHandler<in TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
