using DrMeet.Api.Shared.Domian.Doctors;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetParsizTebDoctorResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public GenderModel? Gender { get; set; }
    public string NationalCode { get; set; } = string.Empty;
    public string? BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public bool ShowInOnlineReserveTime { get; set; }
    public string Bio { get; set; } = string.Empty;
    public bool OverFifteenYearsExperience { get; set; }
    public DoctorExpertiseResponse Expertise { get; set; } = new();
    public List<DoctorCenterResponse> Centers { get; set; } = [];

    public class DoctorCenterResponse
    {
        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? OfficeId { get; set; }
        public int? ClinicId { get; set; }
        public string CenterId { get; set; }

        public List<DoctorCenterDepartmentResponse> Departments { get; set; } = [];

        public List<DoctorServiceDto> DoctorServices { get; set; } = [];
    }

    public class DoctorCenterDepartmentResponse
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<GetFreeTimesForReserveTimeDto> FreeTimes { get; set; } = [];
    }

    public class DoctorExpertiseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    public class GetFreeTimesForReserveTimeDto
    {
        public DayOfWeek DayOfWeek { get; set; }

        public string Date { get; set; }
        
        public int? CenterDoctorShiftRangeId { get; set; }
        
        public List<FreeTimeDto> FreeTimes { get; set; } = [];
    }


    public class FreeTimeDto
    {
        public Guid ShiftId { get; set; }

        public TimeSpan Time { get; set; }

        public bool ReservedBefore { get; set; }
        
       // public int CenterDoctorShiftRangeId { get; set; }
    }

    public class DoctorServiceDto
    {
        public string GroupName { get; set; } = string.Empty;
        public string ServiceDetailId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}