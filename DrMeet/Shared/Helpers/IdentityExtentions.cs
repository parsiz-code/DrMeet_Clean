using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Services.JwtService;
using System.Security.Claims;

namespace DrMeet.Api.Shared.Helpers
{
    public static class IdentityExtentions
    {
        public static int GetId(this ClaimsPrincipal? user, UserType type)

        {
            if (user == null)
                return 0;
            int.TryParse(user.FindFirst(CustomClaims.UserId)?.Value, out var res);
            return res;
        }
    }
}
