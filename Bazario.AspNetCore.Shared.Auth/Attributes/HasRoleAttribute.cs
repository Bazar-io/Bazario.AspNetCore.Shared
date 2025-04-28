using Bazario.AspNetCore.Shared.Auth.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Bazario.AspNetCore.Shared.Auth.Attributes
{
    public sealed class HasRoleAttribute : AuthorizeAttribute
    {
        public HasRoleAttribute(Role role)
            : base(role.ToString())
        { }
    }
}
