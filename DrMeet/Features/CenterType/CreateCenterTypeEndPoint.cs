using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.CenterType;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.CenterTypes;
public static class CreateCenterTypeEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateCenterType", handler: async (
                    ICenterTypeService service,
                      [FromBody] CreateCenterTypeRequestDto request
                ) =>
            {
       

                var result = await service.CreateCenterTypeAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateCenterTypeRequestDto>());

        }
    }
}