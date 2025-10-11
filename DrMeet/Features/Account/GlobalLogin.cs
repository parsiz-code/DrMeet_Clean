using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;

using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Api.Shared.Services.UserService;
using DrMeet.Domain.Enums;

namespace DrMeet.Api.Features.Account;

public static class GlobalLogin
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/GlobalLogin", handler: async (
                    IDoctorService doctorService,
                     ICenterService centerService,
                     IUserService userService,
                     IParsizTebApiService parsizTebApiService,
                     IPatientService patientService,
                    IJwtService jwtService,
                    GlobalLoginDto request
                ) =>
            {

                var user = await userService.LoginAsync(new UserLoginRequestDto { Username = request.Username, Password = request.Password });
               if (user.userId==0)
                    return BadRequest("احراز هویت انجام نشد");
            
                 if (user.userType == UserType.Patient)
                {
                    //if (!request.PatientType.HasValue)
                    //    return BadRequest("نوع بیمار مشخص نشده است");


                    var result = await userService.LoginAllAsync(new UserLoginRequestDto {  Username = request.Username, Password = request.Password });
                    if (result is null)
                        return BadRequest("احراز هویت انجام نشد");

                    var token = jwtService.CreateToken(new LoginTokenRequest
                    {
                        userType = UserType.Patient,  
                        Id = result.Id,   
                        UserId = result.UserId,   
                    });

                    return Ok(new
                    {
                        token
                    });


                }
                else if (user.userType == UserType.Center)
                {
                    var result = await userService.LoginAllAsync(new UserLoginRequestDto { Username = request.Username, Password = request.Password });
                    if (result is null)
                        return BadRequest("احراز هویت انجام نشد");
                    var token = jwtService.CreateToken(new LoginTokenRequest { Id=result.Id, UserId = result.UserId, userType=result.userType });

                    return Ok(new
                    {
                        token
                    });
                }

                else if (user.userType == UserType.Admin)
                {
                    var result = await userService.LoginAllAsync(new UserLoginRequestDto { Username = request.Username, Password = request.Password });
                    if (result is null)
                        return BadRequest("احراز هویت انجام نشد");
                    var token = jwtService.CreateToken(new LoginTokenRequest { Id = result.Id, UserId = result.UserId, userType = result.userType });

                    return Ok(new
                    {
                        token
                    });
                }
                else
                    return BadRequest("احراز هویت انجام نشد");


            })
                .WithTags(ApiInfo.Tag);
        }
    }
}