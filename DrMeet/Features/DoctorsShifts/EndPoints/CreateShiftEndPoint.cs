using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.DoctorShifts;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsShifts.EndPoints;
public static class CreateShiftEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateShift", handler: async (
                    IDoctorShiftService service,
                           IUserService userService, 
                         
                    [FromBody] CreateShiftRequestDto request,
                     HttpContext httpContext
                ) =>
            {
                ReturnUiResult? result = new ReturnUiResult();
                result.ReturnResult = ReturnResult.Error;

                var userType = httpContext.User.GetAuthorizedUserType();
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(), Id);

                if (!userType.IsNullOrEmpty())
                    result = await service.CreateDoctorShiftAsync(request, userId);


                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<CreateShiftRequestDto>())
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