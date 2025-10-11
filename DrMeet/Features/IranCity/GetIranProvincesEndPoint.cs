using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Features.IranCitys;
using DrMeet.Api.Features.IranCitys.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranCity;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsIranCitys.EndPoints;
public static class GetIranCitysEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetIranCitys", async (IIranCityService service,
                [AsParameters] GetIranCityRequestResponseParams request) =>
            {
      

                var data = await service.GetIranCitys(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<GetIranCityRequestResponseParams>());
        }
    }


}