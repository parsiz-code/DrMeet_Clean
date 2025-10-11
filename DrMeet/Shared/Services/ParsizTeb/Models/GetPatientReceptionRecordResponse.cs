using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetPatientReceptionRecordParams : PagedParamData
{
    public string? CenterId { get; set; }
}

public class GetPatientReceptionRecordResponse
{
    public string CenterName { get; set; } = string.Empty;
    public string CenterId { get; set; } = string.Empty;

    public string DoctorName { get; set; } = string.Empty;
    public int ReceptionId { get; set; }
    public int? ReceptionCustomId { get; set; }
    public DateTime ReceptionDate { get; set; }
}
