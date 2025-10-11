using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.DoctorTariffs.DTOs;
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
public class EditDoctorTariffEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/EditDoctorTariff", handler: async (
                    IDoctorTariffService service,
                    [FromBody] UpdateDoctorTariffRequestDto request,
                                 HttpContext httpContext
                ) =>
            {
                var user = httpContext.User;
                var usertype = user.GetAuthorizedUserType().ToEnum<UserType>();
                var doctorId = user.GetAuthorizedUserId(usertype);
        
                if (usertype == UserType.Center && doctorId != request.CenterId)
                    return BadRequest("دسترسی ندارید");

                if (request is null)
                    return Results.BadRequest("درخواست نا معتبر است");

                var result = await service.EditDoctorTariff(request);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<UpdateDoctorTariffRequestDto>())
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
            ;

        }
    }
}