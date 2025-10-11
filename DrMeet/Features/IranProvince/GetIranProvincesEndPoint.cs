using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Features.IranProvinces;
using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranProvince;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsIranProvinces.EndPoints;
public static class GetIranProvincesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetIranProvinces", async (IIranProvinceService service,
                [AsParameters] GetIranProvinceRequestResponseParams request) =>
            {
   

                var data = await service.GetIranProvinces(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<GetIranProvinceRequestResponseParams>());
        }
    }


}