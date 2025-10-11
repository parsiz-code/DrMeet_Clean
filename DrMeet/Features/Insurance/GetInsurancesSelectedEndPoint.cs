using DrMeet.Api.Features.Insurances;
using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Insurances;
namespace DrMeet.Api.Features.DoctorsInsurances.EndPoints;

public static class GetInsurancesSelectedEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetInsurancesSelectedList", async (IInsuranceService service,
                [AsParameters] GetInsuranceSelectListRequestResponseParams request) =>
            {
                var data = await service.GetInsurances(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}