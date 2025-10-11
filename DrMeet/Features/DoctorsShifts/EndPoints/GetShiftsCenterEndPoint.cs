using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorShifts;
using static DrMeet.Api.Shared.Services.DoctorShifts.DoctorShiftService;
namespace DrMeet.Api.Features.DoctorsShifts.EndPoints;

public static class GetShiftsCenterEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterShifts", handler: async (
                    IDoctorShiftService service, [AsParameters] GetShiftCenterRequestDto request
                ) =>
            {
                (bool isValid, string errorMessage) resultError =
                              MapEndpointValidationResult<GetShiftCenterRequestDto>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetShifts(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}