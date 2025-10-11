using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.CenterTypes.DTOs;

public class GetCenterTypeRequestResponseParams : PagedParamData
{
    public string? Title { get; set; }
    public bool Status { get; set; }
}
