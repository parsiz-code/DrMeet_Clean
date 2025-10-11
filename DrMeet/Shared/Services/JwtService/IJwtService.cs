using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;

namespace DrMeet.Api.Shared.Services.JwtService;

public interface IJwtService
{
    JwtToken CreateToken(LoginTokenRequest request);

    string GetClaim(string token, string claimType);
    (UserType userType,int userId, int id) ExteractToken(string token);
}