using Microsoft.AspNetCore.Authorization;

namespace Bazario.AspNetCore.Shared.Authorization.Attributes
{
    public sealed class HasPemissionAttribute : AuthorizeAttribute
    {
        public HasPemissionAttribute(Permission permission)
            : base(permission.ToString())
        { }
    }
}
