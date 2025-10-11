using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Sliders;
public class EditSliderEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditSlider", handler: async (
                    ISliderService service, IUserService userService,
                       [FromBody] UpdateSliderRequestDto request,
                     HttpContext httpContext
                ) =>
            {

                ReturnUiResult? result = new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;

                var userType = httpContext.User.GetAuthorizedUserType();
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(), Id);

                if (!userType.IsNullOrEmpty())
                    result = await service.EditSlider(request, userId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                
                .WithTags(ApiInfo.Tag)
                .AddEndpointFilter(new ValidationFilter<UpdateSliderRequestDto>())
                .RequireAuthorization()
                .AddEndpointFilter(async (context, next) =>
                {
                    var user = context.HttpContext.User;
                    var doctorId = user.GetAuthorizedUserId(UserType.Doctor);

                    if (doctorId is null)
                        return BadRequest("دسترسی ندارید");

                    return await next(context);
                }); 

        }
    }
}