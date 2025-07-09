namespace Bazario.AspNetCore.Shared.Authorization
{
    /// <summary>
    /// Represents the different permissions that can be assigned to users in the system.
    /// </summary>
    public enum Permission
    {
        Authenticated,
        User,
        Admin,
        Owner,
        ChangeOwnData,
        ManageUsers,
        ManageAdmins,
        ManageInternalResources
    }
}
