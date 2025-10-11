using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;

namespace DrMeet.Api.Features.Patients;

public static class GetReceptionRecords
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/ReceptionRecord", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IUserService userService,
                    [AsParameters] GetPatientReceptionRecordParams request
                ) =>
            {
                var patientId = userService.GetPatientId();
                if (patientId == 0)
                    return BadRequest("بیماری یافت نشد");

                var result =
                        await parsizTebApi.GetPatientReceptionRecordsAsync(request, patientId);
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}