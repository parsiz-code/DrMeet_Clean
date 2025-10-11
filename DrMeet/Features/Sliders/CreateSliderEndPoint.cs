using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Polly;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Sliders;
public static class CreateSliderEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateSlider", handler: async (
                    ISliderService service,
                    IUserService userService,
                       [FromBody] CreateSliderRequestDto request,
                     HttpContext httpContext
                ) =>
            {
                ReturnUiResult? result = new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;

                var userType = httpContext.User.GetAuthorizedUserType();
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(), Id);

                if (!userType.IsNullOrEmpty())
                    result = await service.CreateSliderAsync(request, userId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<CreateSliderRequestDto>())
            .RequireAuthorization()
            .AddEndpointFilter(async (context, next) =>
            {
                var user = context.HttpContext.User;
                var usertype = user.GetAuthorizedUserType().ToEnum<UserType>();
                var doctorId = user.GetAuthorizedUserId(usertype);


                if (doctorId is null || (
                    usertype != UserType.Admin &&
                    usertype != UserType.Doctor &&
                    usertype != UserType.Center))
                    return BadRequest("دسترسی ندارید");

                return await next(context);
            }); ;

        }
    }
}