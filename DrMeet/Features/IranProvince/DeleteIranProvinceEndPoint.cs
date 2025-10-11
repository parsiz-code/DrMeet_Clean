using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranProvince;

namespace DrMeet.Api.Features.IranProvinces;
public class DeleteIranProvinceEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteIranProvince/{{IranProvinceId}}", handler: async (
                    IIranProvinceService service,
                    int IranProvinceId
                ) =>
            {
                var result = await service.DeleteIranProvince(IranProvinceId);

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