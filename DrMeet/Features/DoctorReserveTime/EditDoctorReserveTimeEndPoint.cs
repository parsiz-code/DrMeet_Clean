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
public class EditDoctorReserveTimeEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditDoctorReserveTime", handler: async (
                    IDoctorReserveTimeService service,
                    [FromBody] UpdateDoctorReserveTimeRequestDto request
                ) =>
            {
                if (request is null)
                    return Results.BadRequest("درخواست نا معتبر است");

                var result = await service.EditDoctorReserveTime(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<UpdateDoctorReserveTimeRequestDto>());

        }
    }
}