using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Blogs;
public class EditBlogEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditBlog", handler: async (
                    IBlogService service,
                    IUserService userService,
                      IDoctorService doctorService,
                       [FromBody] UpdateBlogRequestDto request,
                    HttpContext httpContext
                ) =>
            {



                ReturnUiResult? result = new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;

                var userType = httpContext.User.GetAuthorizedUserType();
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(), Id);


                if (!userType.IsNullOrEmpty())
                    result = await service.EditBlog(request, userId);
       
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<UpdateBlogRequestDto>())
            .RequireAuthorization()
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