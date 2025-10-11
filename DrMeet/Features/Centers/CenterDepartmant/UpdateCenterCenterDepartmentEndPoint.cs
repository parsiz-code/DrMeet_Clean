using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Centers.CenterDepartmant;

public static class UpdateCenterCenterDepartmentEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateCenterDepartment", handler: async (
                    ICenterService service,
                          IUserService userService,
                    [FromBody] CreateCenterDepartmentDto request, HttpContext httpContext
                ) =>
            {


                var user = httpContext.User;
                var usertype = user.GetAuthorizedUserType().ToEnum<UserType>();
                var userId = user.GetAuthorizedUserId(usertype);

                if (!await userService.UserInCenter(userId.Value, request.CenterId))
                    return BadRequest("دسترسی ندارید");
                var result = await service.UpdateCenterDepartmentAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })

            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<CreateCenterDepartmentDto>());

        }
    }
}




