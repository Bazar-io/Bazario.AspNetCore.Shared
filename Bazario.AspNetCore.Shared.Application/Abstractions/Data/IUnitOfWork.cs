namespace Bazario.AspNetCore.Shared.Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
