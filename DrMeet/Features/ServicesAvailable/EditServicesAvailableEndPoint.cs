using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.ServicesAvailable;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.ServicesAvailables;
public class EditServicesAvailableEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditServicesAvailable", handler: async (
                    IServicesAvailableService service,
                    [FromBody] UpdateServicesAvailableRequestDto request
                ) =>
            {
                

                var result = await service.EditServicesAvailable(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<UpdateServicesAvailableRequestDto>())
            .RequireAuthorization()
            .AddEndpointFilter(async (context, next) =>
            {
                var user = context.HttpContext.User;
                var doctorId = user.GetAuthorizedUserId(UserType.Admin);

                if (doctorId is null)
                    return BadRequest("دسترسی ندارید");

                return await next(context);
            });

        }
    }
}