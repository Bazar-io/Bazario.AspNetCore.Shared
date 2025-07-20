namespace Bazario.AspNetCore.Shared.Abstractions.Data
{
    /// <summary>
    /// Defines a contract for a unit of work that encapsulates a set of operations
    /// </summary>
    /// <remarks>
    /// This interface is typically implemented by a class that manages the database context
    /// </remarks>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes made to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>
        /// 0 - if no changes were made.
        /// 1 - if changes were successfully saved.
        /// </returns>
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
