using DrMeet.Api.Features.CenterTypes;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.CenterType;
namespace DrMeet.Api.Features.DoctorsCenterTypes.EndPoints;
public static class GetCenterTypeByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterType/{{CenterTypeId}}", handler: async (
                    ICenterTypeService service,
                    int CenterTypeId
                ) =>
            {
                var result = await service.GetCenterType(CenterTypeId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}