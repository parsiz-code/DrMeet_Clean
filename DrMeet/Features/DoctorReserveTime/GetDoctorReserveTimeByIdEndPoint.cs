using DrMeet.Api.Features.DoctorReserveTimes;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
namespace DrMeet.Api.Features.DoctorsDoctorReserveTimes.EndPoints;
public static class GetDoctorReserveTimeByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorReserveTime/{{DoctorReserveTimeId}}", handler: async (
                    IDoctorReserveTimeService service,
                    int DoctorReserveTimeId
                ) =>
            {
                var result = await service.GetDoctorReserveTime(DoctorReserveTimeId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}