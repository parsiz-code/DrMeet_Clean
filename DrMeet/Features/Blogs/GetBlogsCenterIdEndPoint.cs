using DrMeet.Api.Features.Blogs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Blogs;
namespace DrMeet.Api.Features.DoctorsBlogs.EndPoints;

public static class GetBlogsCenterIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetBlogByCenterId", handler: async (
                    IBlogService service,
                    [AsParameters] GetBlogByCenterIdRequestResponseParams request

                ) =>
            {

                var data = await service.GetBlogs(request);
                if (data is null)
                    return BadRequest("داده ای یافت است");
                return Ok(data);
            })
                .WithTags(ApiInfo.Tag);

        }
    }
}