using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DrMeet.Api.Features.SeedData.DTOs;

public class CenterServiceSeeDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } // _id از نوع ObjectId در MongoDB
    public int? ClinicId { get; set; } // ممکنه null باشه
    public int? OfficeId { get; set; }

    public string? ClinicName { get; set; } // ممکنه null باشه
    public string? OfficeName { get; set; }

    public int CityId { get; set; }
    public string? CityName { get; set; }

    public int StateId { get; set; }
    public string? StateName { get; set; }

    public int DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public string? ServiceName { get; set; }
}


public class DoctorSeeDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int DoctorId { get; set; }
    public int? ClinicId { get; set; }
    public int? OfficeId { get; set; }
    public int departmentId { get; set; }
    public int DayVisitTime { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NationalCode { get; set; }
    public string? Mobile { get; set; }
    public string? EmergencyPhone { get; set; }

    public string? ExpertiseName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? FatherName { get; set; }
    public int Gender { get; set; }

    public string? NumberMedicalSystem { get; set; }
    public string? Email { get; set; }
    public string? WebSite { get; set; }
    public string? Bio { get; set; }
                 
    public string? OverFifteenYearsExperience { get; set; }
                 
    public string? city { get; set; }
    public string? state { get; set; }
    public string? OfficeName { get; set; }
    public string? ClinicName { get; set; }
}

public class PatientSeeDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? UserName { get; set; } 
    public string? NationalCode { get; set; } 
    public int UserId { get; set; }
    public int PatientId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }

    public string? Mobile { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }

    public int? ClinicId { get; set; }
    public int? OfficeId { get; set; }

    public string? ClinicName { get; set; }
    public string? OfficeName { get; set; }
    public int? Gender { get; set; }
    public int? InsuranceId { get; set; }
    public int? SupplementInuranceId { get; set; }

    public string? Insurance { get; set; }
    public string? SupplementInurance { get; set; }
}
