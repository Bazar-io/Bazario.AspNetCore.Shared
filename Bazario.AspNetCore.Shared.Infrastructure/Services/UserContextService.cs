using Bazario.AspNetCore.Shared.Application.Abstractions.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Bazario.AspNetCore.Shared.Infrastructure.Services
{
    public sealed class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetAuthenticatedUserId()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User;

            if (userClaims?.Identity is null || !userClaims.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = userClaims.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                throw new UnauthorizedAccessException("User ID claim not found.");
            }

            var userIdString = userIdClaim.Value;

            if (!Guid.TryParse(userIdString, out var userId))
            {
                throw new UnauthorizedAccessException("User ID is not a valid GUID.");
            }

            return userId;
        }
    }
}
