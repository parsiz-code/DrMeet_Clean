using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorReserveTimes;
public static class CreateDoctorReserveTimeEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateDoctorReserveTime", handler: async (
                    IDoctorReserveTimeService service,
                      [FromBody] CreateDoctorReserveTimeRequestDto request
                ) =>
            {
              
                var result = await service.CreateDoctorReserveTimeAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateDoctorReserveTimeRequestDto>());

        }
    }
}