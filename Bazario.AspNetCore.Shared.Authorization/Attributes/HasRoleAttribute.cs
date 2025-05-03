using Bazario.AspNetCore.Shared.Authorization.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Bazario.AspNetCore.Shared.Authorization.Attributes
{
    public sealed class HasRoleAttribute : AuthorizeAttribute
    {
        public HasRoleAttribute(Role role)
            : base(role.ToString())
        { }
    }
}
