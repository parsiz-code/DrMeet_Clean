using DrMeet.Api.Features.Insurances;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Insurances;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
namespace DrMeet.Api.Features.DoctorsInsurances.EndPoints;
public static class GetInsuranceByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetInsurance/{{InsuranceId}}", handler: async (
                    IInsuranceService service,
                    int InsuranceId
                ) =>
            {
                var result = await service.GetInsurance(InsuranceId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");

                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}
