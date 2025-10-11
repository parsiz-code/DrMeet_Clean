namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetPatientByIdResponse
{
    public InfoDto PersonalInfo { get; set; } = null!;

    public IEnumerable<CenterResponseDto> Centers { get; set; } = [];

    public class InfoDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Picture { get; set; } = string.Empty;
        public string BloodGroup { get; set; } = string.Empty;
        public string BirthPlace { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int? CountChild { get; set; }
        public string HomeAddress { get; set; } = string.Empty;
        public string InsuranceName { get; set; } = string.Empty;
        public string InsuranceFundsName { get; set; } = string.Empty;
        public string SupplementInsuranceName { get; set; } = string.Empty;
        public string ReasonForReference { get; set; } = string.Empty;
        public string SmsPhone { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string AreaName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string? BirthDate { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string AwarenessMethod { get; set; } = string.Empty;
    }

    public class CenterResponseDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
    }
}