using DNTCommon.Web.Core;
using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Features.Patients.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.DoctorShifts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrMeet.Api.Features.Patients;

public static class GetPatientById
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Detail", handler: async (
               [FromServices] IParsizTebApiService parsizTebApi,
                   [FromServices] IUserService userService,
                [FromBody] UpdatePatientRequestDto dto
                ) =>
                {
                    var patientId = userService.GetPatientId();
                    if (patientId == 0)
                        return BadRequest("بیمار یاقت نشد");

                    var result = await parsizTebApi.GetPatientByIdAsync(patientId);
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}
