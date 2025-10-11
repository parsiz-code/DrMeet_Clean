using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Domian.Doctors;
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
public class EditShiftEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditShift", handler: async (
                    IDoctorShiftService service,
                    [FromBody] UpdateShiftRequestDto request,
                      IDoctorService doctorService,
                      IUserService userService,
                    HttpContext httpContext
                ) =>
            {
              
                if (request is null)
                    return BadRequest("درخواست نا معتبر است.");

                var userType = httpContext.User.GetAuthorizedUserType();
                var Id = httpContext.User.GetId(userType.ToEnum<UserType>());
                var userId = await userService.GetId(userType.ToEnum<UserType>(), Id);

                if (userType.ToEnum<UserType>() == UserType.Doctor)
                {
                    var doctor = await doctorService.GetDoctorByUserId(userId);
                    if (doctor.UserId != userId)
                        return BadRequest("دسترسی ندارید");
                }
                else if (userType.ToEnum<UserType>() == UserType.Center)
                {
                    if (!await doctorService.DoctorIsInCenter(userId, request.DoctorId))
                        return BadRequest("دسترسی ندارید");
                }
                    var result = await service.EditDoctorShift(request, userId);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<UpdateShiftRequestDto>())
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