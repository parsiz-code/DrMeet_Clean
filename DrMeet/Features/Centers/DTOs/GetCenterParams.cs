using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs;


public class GetCenterParams : PagedParamData
{
    public string? Q { get; set; }
}

public class GetCenterDoctorParams : PagedParamData
{
    public string? CenterId { get; set; }
}