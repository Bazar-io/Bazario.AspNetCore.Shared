namespace Bazario.AspNetCore.Shared.Abstractions
{
    /// <summary>
    /// Defines a contract for a service that provides user context information. 
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Gets the ID of the authenticated user.
        /// </summary>
        /// <returns>
        /// A <see cref="Guid"/> representing the ID of the authenticated user.
        /// </returns>
        Guid GetAuthenticatedUserId();
    }
}
