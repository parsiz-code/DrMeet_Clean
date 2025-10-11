using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Patients;

namespace DrMeet.Api.Features.Patients_DrMeet;
public class DeletePatientEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeletePatient/{{PatientId}}", handler: async (
                    IPatientService service,
                    int PatientId
                ) =>
            {
                var result = await service.DeletePatient(PatientId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}