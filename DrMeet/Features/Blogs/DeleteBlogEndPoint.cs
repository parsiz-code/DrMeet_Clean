using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.UserService;
using System.Security.Claims;

namespace DrMeet.Api.Features.Blogs;
public class DeleteBlogEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteBlog/{{BlogId}}", handler: async (
                    IBlogService service,
                    IUserService userService,
                    int BlogId,
                    HttpContext httpContext
                ) =>
            {
               

                var userType = httpContext.User.GetAuthorizedUserType();
                ReturnUiResult result = new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(),Id);
                if (!userType.IsNullOrEmpty())
                    result = await service.DeleteBlog(BlogId, userId);
           

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
               
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag)
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