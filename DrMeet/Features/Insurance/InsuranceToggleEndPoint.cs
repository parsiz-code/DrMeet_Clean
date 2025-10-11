using DrMeet.Api.Features.Insurances;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Insurances;
namespace DrMeet.Api.Features.DoctorsInsurances.EndPoints;

public static class InsuranceToggleEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/InsuranceToggle/{{InsuranceId}}", handler: async (
                    IInsuranceService service,
                    int InsuranceId
                ) =>
            {
                var result = await service.ToggleInsurance(InsuranceId);
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