using DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorOnlineConsultation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorOnlineConsultations;
public static class CreateDoctorOnlineConsultationEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateDoctorOnlineConsultation", handler: async (
                    IDoctorOnlineConsultationService service,
                      [FromBody] CreateDoctorOnlineConsultationRequestDto request
                ) =>
            {
           

                var result = await service.CreateDoctorOnlineConsultationAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateDoctorOnlineConsultationRequestDto>());

        }
    }
}