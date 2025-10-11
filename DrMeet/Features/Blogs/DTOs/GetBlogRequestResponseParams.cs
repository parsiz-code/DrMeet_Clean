using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Blogs.DTOs;

public class GetBlogRequestResponseParams: PagedParamData
{

    public string? Title { get; set; }
}
