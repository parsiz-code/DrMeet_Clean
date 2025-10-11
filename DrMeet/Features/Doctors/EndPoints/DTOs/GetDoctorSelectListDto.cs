namespace DrMeet.Api.Features.Doctors.EndPoints.DTOs;

public class GetDoctorSelectListDto
{
    public string Id { get; set; } = string.Empty;
    
    public int RemoteDoctorId { get; set; }
    
    public string FullName { get; set; } = string.Empty;
    
    public string? Picture { get; set; } = string.Empty;
    
}
