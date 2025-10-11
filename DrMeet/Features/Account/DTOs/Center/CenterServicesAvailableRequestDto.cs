using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs;

public class CenterServicesAvailableRequestDto : PagedParamData
{
    public string? Title { get; set; }
    public bool? Status { get; set; }
}
