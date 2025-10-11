
using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.JwtService;

namespace DrMeet.Api.Features.Account.Center;

public static class GetCenterDetailsPicture
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterDetailsPicture", handler: async (

                     ICenterService centerService,

                    IJwtService jwtService,
                     HttpContext context
                ) =>
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                var model = new DetailsUserResponseDto();

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var result = jwtService.ExteractToken(token);


                if (result.userType == UserType.Center)
                {
                    return Ok(new { userType = result.userType, user = await centerService.GetCenterPicture(result.id) });
                }
                else
                    return BadRequest("توکن نا معتبر است");

            })
                .RequireAuthorization()
                .WithTags(ApiInfo.Tag);
        }
    }
}

