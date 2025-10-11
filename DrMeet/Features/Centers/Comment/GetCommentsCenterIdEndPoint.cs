using DrMeet.Api.Features.Centers.Comment.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Centers;
using DrMeet.Api.Shared.Services.Doctors;

namespace DrMeet.Api.Features.Centers.Comment;

public static class GetCommentsCenterIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCommentsCenterId", handler: async (
                    ICenterService centerService,
                    [AsParameters] GetCommentsCenterIdResponseParams request

                ) =>
            {

                var data = await centerService.GetCommentByCenterAsync(request);
                if (data is null)
                    return BadRequest("داده ای یافت است");
                return Ok(data);
            })
                .WithTags(ApiInfo.Tag);

        }
    }
}
