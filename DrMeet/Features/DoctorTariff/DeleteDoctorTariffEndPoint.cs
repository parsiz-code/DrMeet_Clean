using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.DoctorTariff;
using Microsoft.AspNetCore.Http;
using TeamLibrary.API.Shared.Tools.Helper;

namespace DrMeet.Api.Features.DoctorTariffs;
public class DeleteDoctorTariffEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteDoctorTariff/{{DoctorTariffId}}", handler: async (
                    IDoctorTariffService service,
                    int DoctorTariffId ,
                    HttpContext httpContext
                ) =>
            {

                var user = httpContext.User;
                var usertype = user.GetAuthorizedUserType().ToEnum<UserType>();
                var doctorId = user.GetAuthorizedUserId(usertype);

                var validateCenter = await service.GetDoctorTariff(DoctorTariffId);
                if (usertype == UserType.Center && doctorId != validateCenter.CenterId)
                    return BadRequest("دسترسی ندارید");

                var result = await service.DeleteDoctorTariff(DoctorTariffId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag)
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