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
public class EditDoctorOnlineConsultationEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditDoctorOnlineConsultation", handler: async (
                    IDoctorOnlineConsultationService service,
                    [FromBody] UpdateDoctorOnlineConsultationRequestDto request
                ) =>
            {
       

                var result = await service.EditDoctorOnlineConsultation(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<UpdateDoctorOnlineConsultationRequestDto>());

        }
    }
}