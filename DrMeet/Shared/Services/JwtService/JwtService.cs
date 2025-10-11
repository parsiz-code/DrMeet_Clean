using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace DrMeet.Api.Shared.Services.JwtService;

public class JwtToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpireDate { get; set; }
}

public static class CustomClaims
{
    public const string AdminId = "AdminId";
    public const string CenterId = "CenterId";
    public const string PatientId = "PatientId";
    public const string UserId = "UserId";
    // public const string DoctorId = "DoctorId";
    public const string AccessLevel = "AccessLevel";
}

public class JwtService(IOptions<SiteSetting> options) : IJwtService
{


    #region Create Token    

    public JwtToken CreateToken(LoginTokenRequest request)
    {
        var expiredDate = DateTime.UtcNow.AddDays(1);

        var secretKey = options.Value.SecretKey;
        Claim cliamsUserTypeId = request.userType switch
        {
            UserType.Center => new("Id", request.Id.ToString()),
            UserType.None => throw new NotImplementedException(),
            UserType.Patient => new("Id", request.Id.ToString()),
            UserType.Admin => new("Id", request.Id.ToString()),

        };
        var claims = new List<Claim>
        {

            new (CustomClaims.UserId, request.UserId.ToString()),
            new (CustomClaims.AccessLevel, request.userType.ToString())
        };
        claims.Add(cliamsUserTypeId);
        // if (patientInfo.Centers?.Any() is true)
        // {
        //     claims.AddRange(patientInfo.Centers.Select(item => new Claim(CustomClaims.CenterId, item.CenterId)));
        // }

        var token = GenerateToken(claims, expiredDate, secretKey);

        return new JwtToken
        {
            Token = token,
            ExpireDate = expiredDate
        };
    }

    #endregion

    public string GetClaim(string token, string claimType)
    {
        try
        {
            token = token.Replace("Bearer", "", StringComparison.OrdinalIgnoreCase).Trim();
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            return claims.FindFirst(s => s.Type == claimType)?.Value ?? string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    private string GenerateToken(List<Claim> claims, DateTime expiredDate, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiredDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public (UserType userType, int userId, int id) ExteractToken(string token)
    {
        try
        {
            token = token.Replace("Bearer", "", StringComparison.OrdinalIgnoreCase).Trim();
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
          
            int.TryParse(claims.FindFirstValue(CustomClaims.UserId), out var resultUserId);
            int.TryParse(claims.FindFirstValue("Id"), out var resultId);
            var userType = claims.FindFirstValue(CustomClaims.AccessLevel.ToString()).ToEnum<UserType>();


            return (userType, resultUserId, resultId);
        }
        catch (Exception)
        {

            return (UserType.None, 0, 0);
        }



    }

}