using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.Patients;
using DrMeet.Api.Shared.Services.UserService;

namespace DrMeet.Api.Features.Patients_DrMeet;

public static class Patients_MrMeet
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Detail", handler: async (
                       IPatientService patientService,
                    IUserService userService
                    , int patientId
                ) =>
                {

                    var result = await patientService.GetPatientById(patientId);
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}