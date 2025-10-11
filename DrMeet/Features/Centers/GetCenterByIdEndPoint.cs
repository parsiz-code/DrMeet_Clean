using DrMeet.Api.Features.Centers;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DrMeet.Api.Features.DoctorsCenters.EndPoints;
public static class GetCenterByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenter/{{CenterId}}", handler: async (
                    ICenterService service,
                    int CenterId
                ) =>
            {
                var result = await service.GetCenterDetails(CenterId);
                if (result is null)
                    return BadRequest("مرکز پیدا نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}