using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Domian.Doctors;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.DoctorShifts;
using DrMeet.Api.Shared.Services.UserService;
using System.Security.Claims;

namespace DrMeet.Api.Features.DoctorsShifts.EndPoints;
public class DeleteShiftEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteShift/{{shiftId}}", handler: async (
                    IDoctorShiftService service,
                    int shiftId,
                      IUserService userService,
                      IDoctorService doctorService,
                    HttpContext httpContext
                ) =>
            {
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
                    var centerId = await service.GetCenterId(shiftId);
                    if(centerId!= Id)
                        return BadRequest("دسترسی ندارید");
                    //if (!await doctorService.DoctorIsInCenter(userId,await service.GetDoctorId(shiftId)))
                    //    return BadRequest("دسترسی ندارید");
                }
                var result = await service.DeleteShift(shiftId, await service.GetDoctorId(shiftId));

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag)
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