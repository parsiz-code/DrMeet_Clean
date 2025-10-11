using DrMeet.Api.Features.Blogs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Blogs;
using Microsoft.AspNetCore.Mvc;
namespace DrMeet.Api.Features.DoctorsBlogs.EndPoints;
public static class GetBlogsEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetBlogs", async (IBlogService service, 
                [AsParameters] GetBlogRequestResponseParams request) =>
            {
                var data = await service.GetBlogs(request);
                if (data is null)
                    return BadRequest("داده ای یافت است");
                return Ok(data);
            }).WithTags(ApiInfo.Tag);

           

        }
    }


}