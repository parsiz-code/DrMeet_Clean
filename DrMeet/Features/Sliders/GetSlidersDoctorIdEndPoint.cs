using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Sliders;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Sliders;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace DrMeet.Api.Features.DoctorsSliders.EndPoints;
public static class GetSlidersDoctorIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetSlidersByDoctorId", handler: async (
                    ISliderService service, IUserService userService,
                     [AsParameters] GetSliderByDoctorIdRequestResponseParams request
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
