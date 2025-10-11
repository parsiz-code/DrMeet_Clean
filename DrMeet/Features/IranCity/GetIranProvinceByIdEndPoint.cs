using DrMeet.Api.Features.IranCitys;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.IranCity;
namespace DrMeet.Api.Features.DoctorsIranCitys.EndPoints;
public static class GetIranCityByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetIranCity/{{IranCityId}}", handler: async (
                    IIranCityService service,
                    int IranCityId
                ) =>
            {
                var result = await service.GetIranCity(IranCityId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}