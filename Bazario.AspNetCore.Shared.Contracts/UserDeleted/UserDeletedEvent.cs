namespace Bazario.AspNetCore.Shared.Contracts.UserDeleted
{
    public sealed record UserDeletedEvent(
        Guid UserId);
}
