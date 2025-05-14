namespace Bazario.AspNetCore.Shared.Contracts.UserRegistered
{
    public sealed record UserRegisteredForListingServiceEvent(
        Guid UserId,
        string FirstName,
        string LastName,
        string PhoneNumber);
}
