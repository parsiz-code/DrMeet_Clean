using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.Comment.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.Doctors.Comment.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.BlogsComment;
public static class UpdateStatusCommentBlogEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateStatusCommentBlog", handler: async (
                    IBlogService service,
                      [FromBody] UpdateStatusCommentBlogDto request,
                    HttpContext httpContext
                ) =>
            {
   


                var DoctorId = httpContext.User.GetId(UserType.Doctor);
                var result = await service.UpdateCommentStatusBlogAsync(request, DoctorId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<UpdateStatusCommentBlogDto>())
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