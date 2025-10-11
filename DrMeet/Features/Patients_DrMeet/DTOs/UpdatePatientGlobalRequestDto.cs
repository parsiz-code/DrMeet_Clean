namespace DrMeet.Api.Features.Patients.DTOs;

public class UpdatePatientGlobalRequestDto
{


    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string NationalCode { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public int InsuranceId { get; set; } 
    public int SupplementInsuranceId { get; set; } 
    public string BirthDay { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
}

