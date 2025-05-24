namespace Bazario.AspNetCore.Shared.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
