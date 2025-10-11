using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Insurances;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Insurances;
public static class CreateInsuranceEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateInsurance", handler: async (
                    IInsuranceService service,
                      [FromBody] CreateInsuranceRequestDto request
                ) =>
            {


                var result = await service.CreateInsuranceAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateInsuranceRequestDto>());
        }
    }
}