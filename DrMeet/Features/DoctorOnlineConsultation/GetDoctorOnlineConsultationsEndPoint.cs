using DrMeet.Api.Features.DoctorOnlineConsultations;
using DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorOnlineConsultation;
namespace DrMeet.Api.Features.DoctorsDoctorOnlineConsultations.EndPoints;
public static class GetDoctorOnlineConsultationsEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorOnlineConsultations", async (IDoctorOnlineConsultationService service,
                [AsParameters] GetDoctorOnlineConsultationRequestResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetDoctorOnlineConsultationRequestResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetDoctorOnlineConsultations(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}