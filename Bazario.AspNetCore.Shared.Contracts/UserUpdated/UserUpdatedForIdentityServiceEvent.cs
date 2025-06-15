namespace Bazario.AspNetCore.Shared.Contracts.UserUpdated
{
    public sealed record UserUpdatedForIdentityServiceEvent(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber);
}
