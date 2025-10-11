using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Features.ServicesAvailables;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.ServicesAvailable;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorsServicesAvailables.EndPoints;

public static class GetSelectListServicesAvailablesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetServicesAvailablesSelectList", async (IServicesAvailableService service,
                [AsParameters] GetServicesAvailableSelectListRequestResponseParams request) =>
            {
      

                var data = await service.GetServicesAvailables(request);
                if (data is null)
                    return BadRequest("موردی یافت نشد");
                return Ok(data);
            }).WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<GetServicesAvailableSelectListRequestResponseParams>());
        }
    }


}