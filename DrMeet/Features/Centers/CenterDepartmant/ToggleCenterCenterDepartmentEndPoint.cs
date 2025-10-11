using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Centers.CenterDepartmant;

public static class ToggleCenterCenterDepartmentEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/ToggleCenterDepartment", handler: async (
                    ICenterService service,
                          IUserService userService,
                    [FromBody] ToggleCenterDepartmentDto request
                ) =>
            {
                var result = await service.UpdateCenterDepartmentAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })

            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<ToggleCenterDepartmentDto>());

        }
    }
}




