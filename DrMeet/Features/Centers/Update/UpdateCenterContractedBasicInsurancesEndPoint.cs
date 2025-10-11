using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Mvc;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Centers.Update;

public static class UpdateCenterContractedBasicInsurancesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateContractedBasicInsurances", handler: async (
                    ICenterService service,
                    [FromBody] UpdateCenterContractedBasicInsurancesDto request
                ) =>
            {
              


                var result = await service.UpdateCenterContractedBasicInsurancesAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })

            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<UpdateCenterContractedBasicInsurancesDto>());

        }
    }
}



