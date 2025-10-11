using DrMeet.Api.Features.ServicesAvailables;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.ServicesAvailable;
namespace DrMeet.Api.Features.Centers.CenterDepartmant;

public static class GetSelectListGetDepartmantEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetDepartmant", async (ICenterService service,
                [AsParameters] GetDoctorCenterDepartmantRequest request) =>
            {
               
                var data = await service.GetDepartmantAsync(request);
                if (data is null)
                    return BadRequest("موردی یافت نشد");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);
        }
    }


}