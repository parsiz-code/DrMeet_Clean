using DrMeet.Api.Features.DoctorOnlineConsultations;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.DoctorOnlineConsultation;
namespace DrMeet.Api.Features.DoctorsDoctorOnlineConsultations.EndPoints;
public static class GetDoctorOnlineConsultationByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorOnlineConsultation/{{DoctorOnlineConsultationId}}", handler: async (
                    IDoctorOnlineConsultationService service,
                    int DoctorOnlineConsultationId
                ) =>
            {
                var result = await service.GetDoctorOnlineConsultation(DoctorOnlineConsultationId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}