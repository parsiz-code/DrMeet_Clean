using DrMeet.Api.Features.Expertises;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Expertise;
namespace DrMeet.Api.Features.DoctorsExpertises.EndPoints;
public static class GetExpertiseByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetExpertise/{{ExpertiseId}}", handler: async (
                    IExpertiseService service,
                    int ExpertiseId
                ) =>
            {
                var result = await service.GetExpertise(ExpertiseId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}