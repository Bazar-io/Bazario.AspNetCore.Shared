namespace Bazario.AspNetCore.Shared.Contracts.UserUpdated
{
    public sealed record UserUpdatedForIdentityServiceEvent(
        Guid UserId,
        string Email,
        string PhoneNumber);
}
