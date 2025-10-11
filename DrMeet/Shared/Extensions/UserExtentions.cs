using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Helpers;
using System.Security.Claims;

namespace DrMeet.Api.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int? GetAuthorizedUserId(this ClaimsPrincipal user, UserType expectedType)
    {
        if (user == null) return null;

        var accessLevel = user.FindFirst("AccessLevel")?.Value;
        if (accessLevel != expectedType.ToString())
            return null;

        return user.GetId(expectedType); // فرض بر اینه که متد GetId از قبل تعریف شده
    }
    public static string? GetAuthorizedUserType(this ClaimsPrincipal user)
    {
        if (user == null) return null;

        var accessLevel = user.FindFirst("AccessLevel")?.Value;

        return accessLevel; // Return User Type
    }
}
