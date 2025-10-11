using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.Centers.QuestionAnswer.DTOs;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Centers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
namespace DrMeet.Api.Features.Centers.QuestionAnswer;
public static class DeleteCenterQuestionAnswerEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/DeleteCenterQuestionAnswer", handler: async (
                    ICenterService service,
                      [FromBody] DeleteCenterQuestionAnswerDto request,
                    HttpContext httpContext
                ) =>
            {
                (bool isValid, string errorMessage) resultError =
                                MapEndpointValidationResult<DeleteCenterQuestionAnswerDto>.Validate(request);

                if (!resultError.isValid)
                    return BadRequest(resultError.errorMessage);


                var CenterId = httpContext.User.GetId(UserType.Center);
                var result = await service.DeleteCreateCenterQuestionAnswerAsyncAsync(request, CenterId);

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