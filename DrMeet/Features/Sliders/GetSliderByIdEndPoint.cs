using DrMeet.Api.Features.Sliders;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DrMeet.Api.Features.DoctorsSliders.EndPoints;
public static class GetSliderByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetSlider/{{SliderId}}", handler: async (
                    ISliderService service, IUserService userService,
                   int SliderId
                ) =>
            {
                var result = await service.GetSlider(SliderId);
                if (result is null)
                    return BadRequest("اسلایدر یافت نشد");
                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}