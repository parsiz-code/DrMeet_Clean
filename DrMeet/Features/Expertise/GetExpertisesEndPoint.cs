using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Features.Expertises;
using DrMeet.Api.Features.Expertises.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Expertise;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsExpertises.EndPoints;
public static class GetExpertisesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetExpertises", async (IExpertiseService service,
                [AsParameters] GetExpertiseResponseParams request) =>
            {
             

                var data = await service.GetExpertises(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<GetExpertiseResponseParams>());
        }
    }


}