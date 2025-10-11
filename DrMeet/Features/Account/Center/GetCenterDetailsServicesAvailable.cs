
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.JwtService;

namespace DrMeet.Api.Features.Account.Center;

public static class GetCenterDetailsServicesAvailable
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterDetailsServicesAvailable", handler: async (

                     ICenterService centerService,

                    IJwtService jwtService,
               [AsParameters] CenterServicesAvailableRequestDto  request,
                     HttpContext context
                ) =>
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                var model = new DetailsUserResponseDto();

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var result = jwtService.ExteractToken(token);


                if (result.userType == UserType.Center)
                {
                    return Ok(new { userType = result.userType, user = await centerService.GetCenterServicesAvailable(result.id,request) });
                }
                else
                    return BadRequest("توکن نا معتبر است");

            })
                .RequireAuthorization()
                .WithTags(ApiInfo.Tag);
        }
    }
}

