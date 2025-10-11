using DrMeet.Api.Features.Sliders;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
namespace DrMeet.Api.Features.DoctorsSliders.EndPoints;

public static class GetSlidersCenterIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetSlidersByCenterId", handler: async (
                    ISliderService service,
                    IUserService userService,
                     [AsParameters] GetSliderByCenterIdRequestResponseParams request
                ) =>
            {
                var data = await service.GetSliders(request);
                if (data is null)
                    return BadRequest("داده ای یافت است");
                return Ok(data);
            })
             .WithTags(ApiInfo.Tag);
        }
    }
}