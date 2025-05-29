namespace Bazario.AspNetCore.Shared.Contracts.AdminRegistered
{
    public sealed record AdminRegisteredForComplaintServiceEvent(
        Guid UserId,
        string FirstName,
        string LastName);
}
