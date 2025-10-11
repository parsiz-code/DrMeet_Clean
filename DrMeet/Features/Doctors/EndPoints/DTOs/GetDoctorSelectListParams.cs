using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Doctors.EndPoints.DTOs;

public class GetDoctorSelectListParams : PagedParamData
{
    public int? CityId { get; set; }
    public int? ExpertiseId { get; set; }
    public string? Q { get; set; }
}