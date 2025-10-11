using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.DoctorShifts;
using static DrMeet.Api.Shared.Services.DoctorShifts.DoctorShiftService;
namespace DrMeet.Api.Features.DoctorsShifts.EndPoints;
public static class GetShiftsEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetShifts", handler: async (
                    IDoctorShiftService service, [AsParameters]  GetShiftDoctorRequestDto request
                ) =>
            {
                (bool isValid, string errorMessage) resultError =
                              MapEndpointValidationResult<GetShiftDoctorRequestDto>.Validate(request);

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
