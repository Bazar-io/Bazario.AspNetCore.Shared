namespace Bazario.AspNetCore.Shared.Contracts.UserUpdated
{
    public sealed record UserUpdatedForListingServiceEvent(
        Guid UserId,
        string FirstName,
        string LastName,
        string PhoneNumber);
}
