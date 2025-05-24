namespace Bazario.AspNetCore.Shared.Contracts.UserRegistered
{
    public sealed record SendConfirmationEmailRequestedEvent(
        string Email,
        string ConfirmationLink);
}
