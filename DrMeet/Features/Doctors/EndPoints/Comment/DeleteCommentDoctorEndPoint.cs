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
public static class DeleteCommentDoctorEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/DeleteCommentDoctor", handler: async (
                    IDoctorService service,
                      [FromBody] DeleteDoctorCommentRequestDto request,
                    HttpContext httpContext
                ) =>
            {
              
                var DoctorId = httpContext.User.GetId(UserType.Doctor);
                var result = await service.DeleteCommentDoctorAsync(request,DoctorId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<DeleteDoctorCommentRequestDto>());

            

        }
    }
}