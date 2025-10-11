using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Insurances;

namespace DrMeet.Api.Features.Insurances;
public class DeleteInsuranceEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteInsurance/{{InsuranceId}}", handler: async (
                    IInsuranceService service,
                    int InsuranceId
                ) =>
            {
                var result = await service.DeleteInsurance(InsuranceId);

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