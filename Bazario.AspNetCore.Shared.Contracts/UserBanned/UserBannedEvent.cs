namespace Bazario.AspNetCore.Shared.Contracts.UserBanned
{
    public sealed record UserBannedEvent(
        Guid UserId,
        DateTime? ExpiresAt);
}
