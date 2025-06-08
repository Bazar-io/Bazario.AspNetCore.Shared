namespace Bazario.AspNetCore.Shared.Abstractions
{
    public interface IUserContextService
    {
        Guid GetAuthenticatedUserId();
    }
}
