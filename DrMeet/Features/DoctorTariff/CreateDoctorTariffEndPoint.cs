using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorTariff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.DoctorTariffs;
public static class CreateDoctorTariffEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateDoctorTariff", handler: async (
                    IDoctorTariffService service,
                      [FromBody] CreateDoctorTariffRequestDto request,
                                 HttpContext httpContext
                ) =>
            {
           
                var result = await service.CreateDoctorTariffAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<CreateDoctorTariffRequestDto>())
            .RequireAuthorization()
            .AddEndpointFilter(async (context, next) =>
            {
                var user = context.HttpContext.User;
                var usertype = user.GetAuthorizedUserType().ToEnum<UserType>();
                var doctorId = user.GetAuthorizedUserId(usertype);


                if (doctorId is null ||
                    usertype != UserType.Admin &&
                    usertype != UserType.Center)
                    return BadRequest("دسترسی ندارید");
                

                return await next(context);
            });

        }
    }
}