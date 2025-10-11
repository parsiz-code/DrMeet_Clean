using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorOnlineConsultation;

namespace DrMeet.Api.Features.DoctorOnlineConsultations;
public class DeleteDoctorOnlineConsultationEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteDoctorOnlineConsultation/{{DoctorOnlineConsultationId}}", handler: async (
                    IDoctorOnlineConsultationService service,
                    int DoctorOnlineConsultationId
                ) =>
            {
                var result = await service.DeleteDoctorOnlineConsultation(DoctorOnlineConsultationId);

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