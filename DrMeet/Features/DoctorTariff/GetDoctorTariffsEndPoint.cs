using DrMeet.Api.Features.DoctorTariffs;
using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorTariff;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsDoctorTariffs.EndPoints;
public static class GetDoctorTariffsEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorTariffs", async (IDoctorTariffService service,
                [AsParameters] GetDoctorTariffRequestResponseParams request) =>
            {
             

                var data = await service.GetDoctorTariffs(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<GetDoctorTariffRequestResponseParams>());
        }
    }


}