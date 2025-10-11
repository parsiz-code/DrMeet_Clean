using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Domian;

using DrMeet.Api.Shared.PagedList;
using static DrMeet.Api.Shared.Services.DoctorShifts.DoctorShiftService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrMeet.Api.Features.Doctors.EndPoints.DTOs;

public class GetDoctorDetailDto
{
    public string? Id { get; set; }
    public string? Picture { get; set; }
    //  public int RemoteDoctorId { get; set; }
    public string FullName { get; set; } = string.Empty;
    // public string NationalCode { get; set; } = string.Empty;
    // public DateTime? BirthDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    //  public string UserName { get; set; } = string.Empty;
    //public string FatherName { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    // public bool ShowInOnlineReserveTime { get; set; }
    // public bool OverFifteenYearsExperience { get; set; }
    public GenderModel? Gender { get; set; }
    // public string ExpertiseId { get; set; } = null!;
    public string ExpertiseName { get; set; } = null!;

    public bool Status { get; set; } = false;
    //public List<CentersDto> Centers { get; set; } = [];
    public List<DepartmentResponseDto> Departments { get; set; } = [];
    public List<ShiftResposneDto> Shifts { get; set; } = [];

}


public class GetDoctorCenterDetailDto
{
    public int? Id { get; set; }
    public string? Picture { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    //  public string UserName { get; set; } = string.Empty;
    //public string FatherName { get; set; } = string.Empty;
    // public bool ShowInOnlineReserveTime { get; set; }
    // public bool OverFifteenYearsExperience { get; set; }
    public GenderModel? Gender { get; set; }
    // public string ExpertiseId { get; set; } = null!;
    public string ExpertiseName { get; set; } = null!;

    public bool Status { get; set; } = false;
    //public List<CentersDto> Centers { get; set; } = [];
    public List<DepartmentAllDto> Departments { get; set; } = [];
    public List<ShiftDoctorCenterDto> Shifts { get; set; } = [];
    public List<GetServicesAvailableListResponseDto> Services { get; set; } = [];

}

public class DoctorCenterDetailsResponse
{
    public GetDoctorDetailDto Doctor{ get; set; }

    public List<ShiftResposneDto> Shifts { get; set; }
}
public class GetDoctorCenterShiftResponseDto
{
    public GetDoctorCenterShiftResponseDto()
    {
        doctorInfo = new DoctorInfoDoctorCenterShiftDto();
        WorkDays = new List<ShiftGroupResponseDto>();
    }
    public DoctorInfoDoctorCenterShiftDto doctorInfo { get; set; }

    public List<ShiftGroupResponseDto> WorkDays { get; set; }
}

public class GetDoctorCenterServieResponseDto
{
    public GetDoctorCenterServieResponseDto()
    {
        doctorInfo = new DoctorInfoDoctorCenterShiftDto();
        Service = new PagedList<GetServicesAvailableListResponseDto>();
    }
    public DoctorInfoDoctorCenterShiftDto doctorInfo { get; set; }

    public PagedList<GetServicesAvailableListResponseDto> Service { get; set; }
}
public class DoctorInfoDoctorCenterShiftDto
{
    public string? FullName { get; set; }
}
public class CentersDto
{
    public int CenterId { get; set; } = 0;
    public string? Name { get; set; }
    public List<DepartmentResponseDto> Departments { get; set; } = [];
}
public class CentersAppintmentResponseDto
{
    public int CenterId { get; set; } 
    public string? Name { get; set; }
    public List<DepartmentResponseDto> Departments { get; set; } = [];
    public DoctorAppointmentResponseDto DoctorAppointment { get; set; } = new DoctorAppointmentResponseDto();
}
public class DepartmentAllDto
{
    public int Id { get; set; }
    public string? Name { get; set; }



}
public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    
    public bool Status { get; set; } = false;

}

public class DepartmentCenterDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Status { get; set; } = false;


}

public class GetDoctorProfileResponseDto
{

    public string? Id { get; set; }
    public string? Picture { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public bool ShowInOnlineReserveTime { get; set; }
    public GenderModel? Gender { get; set; }
    public string ExpertiseName { get; set; } = null!;
    public int ExperienceYears { get; set; } = 0;
    public int CommentCount { get; set; } = 0;
    public int MonthlyVisitCount { get; set; } = 0;
    public int DayVisitTime { get; set; } = 0;
    public string? NumberMedicalSystem { get; set; } = string.Empty;
    public List<CentersAppintmentResponseDto> Centers { get; set; } = [];

    public DoctorCommentScoreResponseDto? Score { get; set; }
    public DoctorAddressResponseDto? Address { get; set; }
 //   public DoctorAppointmentDto? Appointment { get; set; }
    public List<GetServicesAvailableSelectedListResponseDto>? AvailableServices { get; set; }
    public List<DoctorOnlineVisitResponseDto>? OnlineVisit { get; set; }
    public DoctorTextConsultationResponseDto? TextConsultation { get; set; }
    public DoctorTelephoneConsultationResponseDto? TelephoneConsultation { get; set; }

}
public class DoctorCommentScoreResponseDto
{
    public int Count { get; set; }
    public double Average { get; set; }
}

public class DoctorAddressResponseDto
{
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}



public class DoctorAppointmentResponseDto
{
 
    public bool IsAppointmentAvailable { get; set; }
    public string? NextAvailableAppointment { get; set; }
}
public class DoctorOnlineVisitResponseDto
{
    public bool IsOnlineVisitAvailable { get; set; }
    public decimal Price { get; set; }
    public string? ServiceName { get; set; }
}

public class DoctorTextConsultationResponseDto
{
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }

}

public class DoctorTelephoneConsultationResponseDto
{
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
}
