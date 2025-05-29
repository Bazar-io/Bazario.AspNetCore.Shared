namespace Bazario.AspNetCore.Shared.Contracts.AdminRegistered
{
    public sealed record AdminRegisteredForUserServiceEvent(
        Guid UserId,
        string Email,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string PhoneNumber);
}
