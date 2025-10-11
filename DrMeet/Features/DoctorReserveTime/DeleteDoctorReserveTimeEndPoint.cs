using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorReserveTime;

namespace DrMeet.Api.Features.DoctorReserveTimes;
public class DeleteDoctorReserveTimeEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteDoctorReserveTime/{{DoctorReserveTimeId}}", handler: async (
                    IDoctorReserveTimeService service,
                    int DoctorReserveTimeId
                ) =>
            {
                var result = await service.DeleteDoctorReserveTime(DoctorReserveTimeId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}