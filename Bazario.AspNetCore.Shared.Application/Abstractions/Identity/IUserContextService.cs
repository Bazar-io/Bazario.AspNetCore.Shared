namespace Bazario.AspNetCore.Shared.Application.Abstractions.Identity
{
    public interface IUserContextService
    {
        Guid GetAuthenticatedUserId();
    }
}
