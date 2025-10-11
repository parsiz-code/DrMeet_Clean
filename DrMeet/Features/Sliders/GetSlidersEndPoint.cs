using DrMeet.Api.Features.Sliders;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
namespace DrMeet.Api.Features.DoctorsSliders.EndPoints;
public static class GetSlidersEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetSliders", handler: async (
                    ISliderService service, IUserService userService,
                     [AsParameters] GetSliderRrequestResponseParams request
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