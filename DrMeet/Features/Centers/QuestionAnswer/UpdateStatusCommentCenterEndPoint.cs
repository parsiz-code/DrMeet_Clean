using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Features.Centers.QuestionAnswer.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace DrMeet.Api.Features.Centers.QuestionAnswer;
public static class UpdateStatusCenterQuestionAnswerEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateStatusCenterQuestionAnswer", handler: async (
                    ICenterService service,
                      [FromBody] UpdateStatusCenterQuestionAnswerDto request,
                    HttpContext httpContext
                ) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<UpdateStatusCenterQuestionAnswerDto>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);


                var CenterId = httpContext.User.GetId(UserType.Center);
                var result = await service.UpdateCommentStatusCenterAsync(request, CenterId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
             .WithTags(ApiInfo.Tag)

            .AddEndpointFilter(async (context, next) =>
            {
                ClaimsPrincipal user = context.HttpContext.User;
                var accessLevel = user.FindFirst("AccessLevel")?.Value;

                var DoctorId = user.GetId(UserType.Doctor);

                if (accessLevel != nameof(UserType.Doctor))
                    return BadRequest("دسترسی ندارید");

                return await next(context);
            });

        }
    }
}