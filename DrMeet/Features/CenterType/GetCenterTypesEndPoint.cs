using DrMeet.Api.Features.CenterTypes;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.CenterType;
namespace DrMeet.Api.Features.DoctorsCenterTypes.EndPoints;
public static class GetCenterTypesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCenterTypes", async (ICenterTypeService service,
                [AsParameters] GetCenterTypeRequestResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetCenterTypeRequestResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetCenterTypes(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}