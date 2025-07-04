namespace Bazario.AspNetCore.Shared.Authorization
{
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
