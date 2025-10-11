namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class CreatePatientRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public int CenterId { get; set; }
}

public class CreatePatientResponse
{
    public int Id { get; set; } 
}