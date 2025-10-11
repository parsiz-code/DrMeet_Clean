using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Blogs.DTOs;

public class GetBlogByDoctorIdRequestResponseParams : PagedParamData
{
    public int DoctorId { get; set; }
}
public class GetBlogByCenterIdRequestResponseParams : PagedParamData
{
    public int CenterId { get; set; }
}
