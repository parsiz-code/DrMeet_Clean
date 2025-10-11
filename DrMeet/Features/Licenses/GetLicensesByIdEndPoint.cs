using DrMeet.Api.Features.Licenses;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Licensess;
namespace DrMeet.Api.Features.DoctorsLicenses.EndPoints;
public static class GetLicensesByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetLicenses/{{LicensesId}}", handler: async (
                    ILicensesService service,
                    int LicensesId
                ) =>
            {
                var result = await service.GetLicenses(LicensesId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}

