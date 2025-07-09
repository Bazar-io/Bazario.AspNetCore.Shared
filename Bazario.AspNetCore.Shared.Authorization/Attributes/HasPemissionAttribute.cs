using Microsoft.AspNetCore.Authorization;

namespace Bazario.AspNetCore.Shared.Authorization.Attributes
{
    /// <summary>
    /// Attribute to specify that a user must have a specific permission to access a resource.
    /// </summary>
    public sealed class HasPemissionAttribute : AuthorizeAttribute
    {
        public HasPemissionAttribute(Permission permission)
            : base(permission.ToString())
        { }
    }
}
