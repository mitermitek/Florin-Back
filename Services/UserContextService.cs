using System.Security.Claims;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public long GetUserId()
    {
        var user = httpContextAccessor.HttpContext!.User;
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        return long.Parse(userIdClaim!.Value);
    }
}
