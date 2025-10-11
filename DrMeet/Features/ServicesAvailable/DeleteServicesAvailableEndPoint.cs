using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.ServicesAvailable;

namespace DrMeet.Api.Features.ServicesAvailables;
public class DeleteServicesAvailableEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteServicesAvailable/{{ServicesAvailableId}}", handler: async (
                    IServicesAvailableService service,
                    int ServicesAvailableId
                ) =>
            {
                var result = await service.DeleteServicesAvailable(ServicesAvailableId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(string.Join(",", result.LstMessage));
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization()
                .AddEndpointFilter(async (context, next) =>
                 {
                     var user = context.HttpContext.User;
                     var doctorId = user.GetAuthorizedUserId(UserType.Admin);

                     if (doctorId is null)
                         return BadRequest("دسترسی ندارید");

                     return await next(context);
                 }); ;
        }
    }
}