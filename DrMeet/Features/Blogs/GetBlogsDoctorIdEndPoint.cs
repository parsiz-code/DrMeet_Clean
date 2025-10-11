using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.Blogs;
using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace DrMeet.Api.Features.DoctorsBlogs.EndPoints;
public static class GetBlogsDoctorIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetBlogByDoctorId", handler: async (
                    IBlogService service,
                    [AsParameters] GetBlogByDoctorIdRequestResponseParams request
                   
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
