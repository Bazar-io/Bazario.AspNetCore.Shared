namespace Bazario.AspNetCore.Shared.Infrastructure.Abstractions
{
    public interface IUserContextService
    {
        Guid GetAuthenticatedUserId();
    }
}
