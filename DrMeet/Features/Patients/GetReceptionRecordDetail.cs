using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;

namespace DrMeet.Api.Features.Patients;

public static class GetPatientReceptionRecords
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/ReceptionRecord/{{receptionId}}", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IUserService userService,
                    int receptionId,
                    string centerId

                ) =>
                {
                    var result = await parsizTebApi.GetReceptionDetailsAsync(centerId,userService.GetPatientId(),receptionId);
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}