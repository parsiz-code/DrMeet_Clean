using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Features.ServicesAvailables;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.ServicesAvailable;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsServicesAvailables.EndPoints;
public static class GetServicesAvailablesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetServicesAvailables", async (IServicesAvailableService service,
                [AsParameters] GetServicesAvailableRequestResponseParams request) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<GetServicesAvailableRequestResponseParams>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                var data = await service.GetServicesAvailables(request);
                if (data is null)
                    return BadRequest("موردی یافت نشد");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}
public static class GetServicesAvailablesRequredDoctorIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDoctorServicesAvailables", async (IServicesAvailableService service,
                [AsParameters] GetServicesAvailableRequiedDoctorIdRequestResponseParams request) =>
            {
                var data = await service.GetServicesDoctorInfoAvailables(request);
                if (data is null)
                    return BadRequest("موردی یافت نشد");
                return Ok(data);
            })
                .WithTags(ApiInfo.Tag)
                .AddEndpointFilter(new ValidationFilter<GetServicesAvailableRequiedDoctorIdRequestResponseParams>());
        }
    }


}
