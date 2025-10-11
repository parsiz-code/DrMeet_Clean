namespace DrMeet.Api.Features.Doctors.EndPoints.DTOs;

public class GetDoctorListDto
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
    public required string Picture { get; set; }
    public string Expertise { get; set; }
    public int ExperienceYears { get; set; }
    public DoctorScore Score { get; set; }
    public AddressDto Address { get; set; }
    public Appointment Appointment { get; set; }
    public OnlineVisit OnlineVisit { get; set; }

    
    
    public List<WorkType> WorkTypes { get; set; } =
    [
        new()
        {
            Name = "حضوری",
            Icon = "lucide-hospital"
        },
        new()
        {
            Name = "تلفنی",
            Icon = "lucide-phone"
        },
        new ()
        {
            Name = "متنی",
            Icon = "lucide-message-circle"
        },
        new ()
        {
            Name = "تصویری",
            Icon = "lucide-video "
        }
    ];
}

public class DoctorScore
{
    public double Average { get; set; }
    public int Count { get; set; }
}

public class AddressDto
{
    public string AddressLine { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}

public class Appointment
{
    public bool IsAppointmentAvailable { get; set; }

    public DateTime? NextAvailableAppointment { get; set; }
}

public class OnlineVisit
{
    public bool IsOnlineVisitAvailable { get; set; }

    public long? Price { get; set; }
}

public class WorkType
{
    public string Icon { get; set; }

    public string Name { get; set; }
}