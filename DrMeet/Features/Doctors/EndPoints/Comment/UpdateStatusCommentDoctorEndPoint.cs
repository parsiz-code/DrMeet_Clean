using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.Doctors.Comment.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Doctors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsComment;
public static class UpdateStatusCommentDoctorEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateStatusCommentDoctor", handler: async (
                    IDoctorService service,
                      [FromBody] UpdateStatusCommentDoctorRequestDto request,
                    HttpContext httpContext
                ) =>
            {
              

                var DoctorId = httpContext.User.GetId(UserType.Doctor);
                var result = await service.UpdateCommentStatusDoctorAsync(request, DoctorId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<UpdateStatusCommentDoctorRequestDto>())

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