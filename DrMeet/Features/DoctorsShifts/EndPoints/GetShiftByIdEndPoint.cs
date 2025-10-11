using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.DoctorShifts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
namespace DrMeet.Api.Features.DoctorsShifts.EndPoints;
public static class GetShiftByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetShift/{{shiftId}}", handler: async (
                    IDoctorShiftService service,
                    int shiftId
                ) =>
            {
                var result = await service.GetShift(shiftId);
                if (result is null)
                    return BadRequest("شیفت یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}