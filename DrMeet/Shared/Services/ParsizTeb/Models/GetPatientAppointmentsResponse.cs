using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetPatientAppointmentsRequestResponseParamsDto : PagedParamData
{
    public int? CenterId { get; set; }
}

public class GetPatientAppointmentsResponse
{
    public int CenterId { get; set; } 
    public string CenterName { get; set; } = string.Empty;
    public string DoctorFullName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeSpan? Time { get; set; }
    public bool IsVisited { get; set; }
}