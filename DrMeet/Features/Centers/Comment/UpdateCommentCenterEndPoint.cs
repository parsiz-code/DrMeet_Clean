using DNTCommon.Web.Core;
using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.CentersComment;
public static class UpdateCommentCenterEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateCommentCenter", handler: async (
                    ICenterService service,
                      [FromBody] UpdateCommentCenterDto request,
                             HttpContext httpContext
                ) =>
            {

                var Id = httpContext.User.GetId(httpContext.User.GetAuthorizedUserType().ToEnum<UserType>());
                var result = await service.UpdateCommentCenterAsync(request, Id, httpContext.User.GetAuthorizedUserType().ToEnum<UserType>());

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)
             .AddEndpointFilter(new ValidationFilter<UpdateCommentCenterDto>())
              .RequireAuthorization(); ;

            

        }
    }
}