using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Centers;
public static class CreateCenterEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateCenter", handler: async (
                    ICenterService service,
                    IUserService userService,
                    IJwtService jwtService,
                    [FromBody] CreateCenterDto request
                ) =>
            {



                var result = await service.CreateCenterAsync(request);
                if (result.IsError)
                {
                    return BadRequest(result.FirstError.Description);
                }
                
                var token = jwtService.CreateToken(new LoginTokenRequest()
                {
                    userType = UserType.Center,
                    UserId=result.Value.userId,
                    Id=result.Value.centerId
              
                });

                return Ok(new
                {
                    token
                });

            })

            .WithTags(ApiInfo.Tag).WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateCenterDto>());
            ;

        }
    }
}