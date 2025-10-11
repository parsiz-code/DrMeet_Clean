using DrMeet.Api.Features.IranProvinces;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.IranProvince;
namespace DrMeet.Api.Features.DoctorsIranProvinces.EndPoints;
public static class GetIranProvinceByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetIranProvince/{{IranProvinceId}}", handler: async (
                    IIranProvinceService service,
                    int IranProvinceId
                ) =>
            {
                var result = await service.GetIranProvince(IranProvinceId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}