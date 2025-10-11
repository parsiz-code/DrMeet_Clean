using DrMeet.Api.Features.IranCitys.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranCity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.IranCitys;
public class EditIranCityEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditIranCity", handler: async (
                    IIranCityService service,
                    [FromBody] UpdateIranCityRequestDto request
                ) =>
            {
     
                if (request is null)
                    return Results.BadRequest("درخواست نا معتبر است");

                var result = await service.EditIranCity(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<UpdateIranCityRequestDto>());

        }
    }
}