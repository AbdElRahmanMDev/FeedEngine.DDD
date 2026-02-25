using BuildingBlocks.Application.Abstraction;
using System.Security.Claims;

namespace FeedEngine.DDD.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true;

        public Guid? UserId
        {
            get
            {
                if (!IsAuthenticated) return null;

                var user = _httpContextAccessor.HttpContext!.User;

                var usercalims = user.FindFirst(ClaimTypes.NameIdentifier)
                    ?? user.FindFirst("sub");

                if (usercalims is null)
                    throw new UnauthorizedAccessException("User ID claim not found.");

                return Guid.Parse(usercalims.Value);
            }
        }

    }

}
