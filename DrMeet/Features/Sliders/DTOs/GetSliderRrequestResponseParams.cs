using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Sliders.DTOs;

public class GetSliderRrequestResponseParams : PagedParamData
{
    public string? Title { get; set; }
}
public class GetSliderByDoctorIdRequestResponseParams : PagedParamData
{
    public int DoctorId { get; set; }
}
public class GetSliderByCenterIdRequestResponseParams : PagedParamData
{
    public int CenterId { get; set; }
}
