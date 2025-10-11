using DrMeet.Api.Features.Licenses;
using DrMeet.Api.Features.Licenses.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Licensess;
namespace DrMeet.Api.Features.DoctorsLicenses.EndPoints;
public static class GetLicensesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetLicenses", async (ILicensesService service,
                [AsParameters] GetLicensesResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetLicensesResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetLicenses(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}

public static class GetLicensesByCenterIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetLicensesByCenterId", async (ILicensesService service,
                [AsParameters] GetLicensesByCenterIdResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetLicensesByCenterIdResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetLicenses(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}