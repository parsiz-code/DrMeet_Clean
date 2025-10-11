using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.ServicesAvailables.DTOs;

public class GetServicesAvailableRequestResponseParams : PagedParamData
{
    public bool? Status { get; set; }
    public string? Title { get; set; }
    public int? DoctorId { get; set; }

}
public class GetServicesAvailableRequiedDoctorIdRequestResponseParams : PagedParamData
{
    public bool? Status { get; set; }
    public string? Title { get; set; }
    public required int DoctorId { get; set; }

}



public class GetServicesAvailableSelectListRequestResponseParams : PagedParamData
{
    public string? Q { get; set; }

}
