namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetPatientMedicalFileResponse
{
    public int Id { get; set; }
    public string File { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public string CenterId { get; set; } = string.Empty;
    public string CreateDate { get; set; } = string.Empty;
}