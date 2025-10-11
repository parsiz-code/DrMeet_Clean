
namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class ReserveTimeDto 
{
    public int DepartmentId { get; set; }
    
    public int DoctorId { get; set; }
    
    public int? PatientId { get; set; }
    
    public string? Comment { get; set; }
    
    public TimeSpan[] RequestTimes { get; set; } = [];
    
    public string RequestDate { get; set; }
    
    public int? CenterDoctorShiftRangeId { get; set; }

    public required int CenterId { get; set; }
}