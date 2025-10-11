using DrMeet.Api.Features.DoctorReserveTimes;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
namespace DrMeet.Api.Features.DoctorsDoctorReserveTimes.EndPoints;
public static class GetDoctorReserveTimesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorReserveTimes", async (IDoctorReserveTimeService service,
                [AsParameters] GetDoctorReserveTimeRequestResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetDoctorReserveTimeRequestResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetDoctorReserveTimes(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}
