using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranCity;

namespace DrMeet.Api.Features.IranCitys;
public class DeleteIranCityEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteIranCity/{{IranCityId}}", handler: async (
                    IIranCityService service,
                    int IranCityId
                ) =>
            {
                var result = await service.DeleteIranCity(IranCityId);

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