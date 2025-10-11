using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Patients.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;

namespace DrMeet.Api.Features.Patients;

public static class EditPatient
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {

            app.MapPost($"{ApiInfo.Prefix}/EditPatient", handler: async (
                 [FromServices] IPatientService patientService,
                 [FromServices] IUserService userService,
                HttpContext httpContext,
               [FromBody] UpdatePatientGlobalRequestDto updatePatientDto

                ) =>
            {

                (bool isValid, string errorMessage) resultError =
                MapEndpointValidationResult<UpdatePatientGlobalRequestDto>.Validate(updatePatientDto);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                if (updatePatientDto is null)
                    return BadRequest("درخواست نا معتبر است.");
                //var patientId = userService.GetPatientId();
                //if (patientId != 0)
                //    return BadRequest("امکان ویرایش این بیمار وجود ندارد");
                var patientId = httpContext.User.GetId(UserType.Patient);


                var result = await patientService.EditPatientGlobal(updatePatientDto, patientId);
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag)
                 .AddEndpointFilter(new ValidationFilter<UpdatePatientGlobalRequestDto>())
                .RequireAuthorization()
                .AddEndpointFilter(async (context, next) =>
                {
                    var user = context.HttpContext.User;
                    var doctorId = user.GetAuthorizedUserId(UserType.Patient);

                    if (doctorId is null)
                        return BadRequest("دسترسی ندارید");

                    return await next(context);
                }); ;
        }
    }
}