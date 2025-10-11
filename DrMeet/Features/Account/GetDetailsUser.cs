
using DrMeet.Api.Features.Account.DTOs;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Api.Shared.Services.UserService;
using Polly;

namespace DrMeet.Api.Features.Account;

public static class GetDetailsUser
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDetailsUser", handler: async (
                     IDoctorService doctorService,
                     ICenterService centerService,
                     IUserService userService,
                     IParsizTebApiService parsizTebApiService,
                     IPatientService patientService,
                    IJwtService jwtService,
                     HttpContext context
                ) =>
                {
                    //  var s =await parsizTebApiService.GetPatientByIdAsync(3);
                    var authHeader = context.Request.Headers["Authorization"].ToString();

                    var model = new DetailsUserResponseDto();

                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    var result = jwtService.ExteractToken(token);

                    if (result.userType == UserType.Doctor)
                    {
                        return Ok(new { userType = result.userType, user = await doctorService.GetDoctorByUserDtoId(result.id) });
                    }
                    else if (result.userType == UserType.Patient)
                    {
                        //bool statusId = int.TryParse(result.id, out var Id);
                        //if (statusId)
                        //{
                        //    return Ok(new { userType = result.userType, user = await patientService.GetPatientDtoByRemoteId(Id) });

                        //}
                        //else
                      //  {
                            return Ok(new { userType = result.userType, user = await patientService.GetPatientDtoById(result.id) });

                        //}
                    }
                    else if (result.userType == UserType.Center)
                    {
                        return Ok(new { userType = result.userType, user = await centerService.GetCenterDetailAsync(result.id) });
                    }

                    else if (result.userType == UserType.Admin)
                    {
                        return Ok(new { userType = result.userType, user = await userService.GetUserDetails(result.id) });
                    }
                    else
                        return BadRequest("توکن نا معتبر است");

                })
                .RequireAuthorization()
                .WithTags(ApiInfo.Tag);
        }
    }
}

