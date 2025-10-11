using DrMeet.Api.Features.Blogs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.UserService;
namespace DrMeet.Api.Features.DoctorsBlogs.EndPoints;
public static class GetBlogByIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetBlog/{{BlogId}}", handler: async (
                    IBlogService service,
                    int BlogId
                ) =>
            {
                var result = await service.GetBlog(BlogId);
                if (result is null)
                    return BadRequest("مقاله یافت نشد");

                return Ok(result);
            })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}