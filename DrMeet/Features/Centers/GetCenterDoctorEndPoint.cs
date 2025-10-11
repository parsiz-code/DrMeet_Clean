using DrMeet.Api.Features.Centers;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DrMeet.Api.Features.DoctorsCenters.EndPoints;
public static class GetCenterDoctorEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterDoctor", handler: async (
                    ICenterService service,
                 [AsParameters] GetDoctorCenterResponseParams request
                ) =>
            {
                var result = await service.GetCenterDoctor(request);
                if (result is null)
                    return BadRequest("دکتر یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}