using DrMeet.Api.Features.Centers;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Mvc;
namespace DrMeet.Api.Features.DoctorsCenters.EndPoints;
public static class GetCentersNearbyEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCentersNearby", async (ICenterService service, 
                [AsParameters] NearbyLocationDto request) =>
            {
                var data = await service.GetCenters(request);
                if (data is null)
                    return BadRequest("خطا رخ داده است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);

           

        }
    }


}