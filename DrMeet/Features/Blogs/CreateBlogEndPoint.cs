using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;




//using ExtentionLibrary.Enums;
using ExtentionLibrary.Strings;
using DNTCommon.Web.Core;
using ErrorOr;
namespace DrMeet.Api.Features.Blogs;
public static class CreateBlogEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateBlog", handler: async (
                    IBlogService service,
                      [FromBody] CreateBlogRequestDto request,
                    HttpContext httpContext
                ) =>
            {

                var userType = httpContext.User.GetAuthorizedUserType();
                ReturnUiResult? result=new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                if (!userType.IsNullOrEmpty() && userType.ToEnum<UserType>() == UserType.Doctor)
                {

                    result = await service.CreateBlogAsync(request, Id);
                }
                else if (!userType.IsNullOrEmpty() && userType.ToEnum<UserType>() == UserType.Center)
                {
  
                    result = await service.CreateBlogCenterAsync(request, Id);
                }

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<CreateBlogRequestDto>())
            .AddEndpointFilter(async (context, next) =>
            {
                var user = context.HttpContext.User;
                var userType = user.GetAuthorizedUserType();
                int? Id = userType.ToEnum<UserType>() switch
                {
                    UserType.Doctor => user.GetAuthorizedUserId(UserType.Doctor),
                    UserType.Center => user.GetAuthorizedUserId(UserType.Center),
                    _ => null
                };
                if (Id is null)
                    return BadRequest("دسترسی ندارید");


                return await next(context);
            });

        }
    }
}