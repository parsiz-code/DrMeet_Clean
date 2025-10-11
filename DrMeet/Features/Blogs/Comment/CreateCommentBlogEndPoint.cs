using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.Comment.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.Doctors.Comment.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.BlogsComment;
public static class CreateCommentBlogEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateCommentBlog", handler: async (
                    IBlogService service,
                      [FromBody] CreateCommentBlogDto request
                      ,
                         HttpContext httpContext
                ) =>
            {
        

                var Id = httpContext.User.GetId(httpContext.User.GetAuthorizedUserType().ToEnum<UserType>());
                var result = await service.CreateCommentBlogAsync(request, Id, httpContext.User.GetAuthorizedUserType().ToEnum<UserType>());

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateCommentBlogDto>());

        }
    }
}