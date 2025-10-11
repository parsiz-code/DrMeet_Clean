using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.Setting;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.SiteSetting;
public class EditSettingEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditSetting", handler: async (
                    ISettingService service,
                    [FromBody] ApplicationSettingRequestDto request
                ) =>
            {
                (bool isValid, string errorMessage) resultError =
                                       MapEndpointValidationResult<ApplicationSettingRequestDto>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);

                if (request is null)
                    return BadRequest("درخواست نا معتبر است");

                var result = await service.EditSetting(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag);

        }
    }
}