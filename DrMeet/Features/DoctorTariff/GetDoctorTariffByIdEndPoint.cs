using DrMeet.Api.Features.DoctorTariffs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.DoctorTariff;
namespace DrMeet.Api.Features.DoctorsDoctorTariffs.EndPoints;
public static class GetDoctorTariffByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorTariff/{{DoctorTariffId}}", handler: async (
                    IDoctorTariffService service,
                    int DoctorTariffId
                ) =>
            {
                var result = await service.GetDoctorTariff(DoctorTariffId);
                if (result is null)
                    return BadRequest("تعرفه ای  یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}