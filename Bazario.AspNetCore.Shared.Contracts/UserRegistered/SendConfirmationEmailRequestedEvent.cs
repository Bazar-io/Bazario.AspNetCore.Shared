namespace Bazario.AspNetCore.Shared.Contracts.UserRegistered
{
    public sealed record SendConfirmationEmailRequestedEvent(
        Guid UserId,
        string Email,
        Guid ConfirmEmailTokenId,
        string ConfirmEmailToken);
}
