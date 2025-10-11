using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs;
public class CenterServicesAvailableDtoResponseDto
{
    //خدمات قابل ارائه
    public PagedList<CenterItemServicesAvailable>? ServicesAvailable { get; set; }
}
