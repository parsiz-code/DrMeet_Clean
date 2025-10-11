using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.CentersComment;
public static class UpdateStatusCommentCenterEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateStatusCommentCenter", handler: async (
                    ICenterService service,
                      [FromBody] UpdateStatusCommentCenterDto request,
                    HttpContext httpContext
                ) =>
            {
          
                var DoctorId = httpContext.User.GetId(UserType.Doctor);
                var result = await service.UpdateCommentStatusCenterAsync(request, DoctorId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<UpdateStatusCommentCenterDto>())
            .AddEndpointFilter(async (context, next) =>
            {
                ClaimsPrincipal user = context.HttpContext.User;
                var accessLevel = user.FindFirst("AccessLevel")?.Value;

                var DoctorId = user.GetId(UserType.Doctor);

                if (accessLevel != nameof(UserType.Doctor))
                    return BadRequest("دسترسی ندارید");

                return await next(context);
            });

        }
    }
}