//using System.ComponentModel.DataAnnotations;

//namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

//public class UpdateProfileDoctorDto
//{

//    [Required(ErrorMessage = "شناسه پزشک الزامی است")]
//    public string? Id { get; set; } = string.Empty;
//    public string? FirstName { get; set; } = string.Empty;

//    public string? LastName { get; set; } = string.Empty;
//    public string? FatherName { get; set; } = string.Empty;
//    public string? Bio { get; set; } = string.Empty;
//    public string? ExpertiseId { get; set; } = null!;
//    public string? ExpertiseName { get; set; } = null!;
//    public string? Descrption { get; set; } = string.Empty;
//    public string? Province { get; set; } = string.Empty;
//    public string? City { get; set; } = string.Empty;
//    public string? Region { get; set; } = string.Empty;
//    public string? Over15YearsOfExperience { get; set; } = string.Empty;
//    public string? Password { get; set; } = string.Empty;
//    public string? BirthDate { get; set; }
//    public string? WebSite { get; set; } = string.Empty;
//    public string? NumberOfMedicalSystem { get; set; } = string.Empty;
//    public string? Email { get; set; } = string.Empty;
//    public IFormFile? Picture { get; set; }

//}


public class UpdateWorkTypesDoctorRequestDto
{
    public int DoctorId { get; set; }
    public bool InPerson { get; set; }
    public decimal PriceInPerson { get; set; } = 0;
    public bool IsVideoConsultation { get; set; }
    public decimal PriceIsVideoConsultation { get; set; } = 0;
    public bool IsPhoneConsultation { get; set; }
    public decimal PriceIsPhoneConsultation { get; set; } = 0;

    public bool IsTextConsultation { get; set; }
    public decimal PriceIsTextConsultation { get; set; } = 0;
}


