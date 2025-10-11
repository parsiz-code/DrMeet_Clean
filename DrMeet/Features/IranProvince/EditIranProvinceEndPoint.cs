using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranProvince;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.IranProvinces;
public class EditIranProvinceEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditIranProvince", handler: async (
                    IIranProvinceService service,
                    [FromBody] UpdateIranProvinceRequestDto request
                ) =>
            {
 
                var result = await service.EditIranProvince(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
               .AddEndpointFilter(new ValidationFilter<UpdateIranProvinceRequestDto>());

        }
    }
}