using DrMeet.Api.Features.DoctorReserveTimes;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
namespace DrMeet.Api.Features.DoctorsDoctorReserveTimes.EndPoints;

public static class GetFreeNearestTimeDoctorReserveTimesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetFreeNearestTimeDoctorReserveTimes", async (IDoctorReserveTimeService service,
                [AsParameters] GetDoctorFreeOneTimeReserveTimeRequestDto request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetDoctorFreeOneTimeReserveTimeRequestDto>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var result = await service.GetDoctorReserveTimes(request);


                if (result.IsError)
                    return BadRequest(string.Join(",", result.Errors.Select(_ => _.Description)));
             
                return Ok(result.Value);
            }).WithTags(ApiInfo.Tag);
        }
    }


}