using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;

namespace DrMeet.Api.Features.Patients;

public static class GetPatientMedicalFiles
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/MedicalFile", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IUserService userService,
                    [AsParameters] GetPatientMedicalFilesParams request 

                ) =>
                {
                    var patientId = userService.GetPatientId();
                    if (patientId == 0)
                        return BadRequest("بیماری یافت نشد");

                    var result = await parsizTebApi.GetPatientMedicalFilesAsync(request,patientId);
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}