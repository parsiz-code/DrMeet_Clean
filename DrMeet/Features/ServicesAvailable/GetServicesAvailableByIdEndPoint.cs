using DrMeet.Api.Features.ServicesAvailables;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ServicesAvailable;
namespace DrMeet.Api.Features.DoctorsServicesAvailables.EndPoints;
public static class GetServicesAvailableByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetServicesAvailable/{{ServicesAvailableId}}", handler: async (
                    IServicesAvailableService service,
                    int ServicesAvailableId
                ) =>
            {
                var result = await service.GetServicesAvailable(ServicesAvailableId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}