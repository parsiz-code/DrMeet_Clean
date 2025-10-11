using DrMeet.Api.Shared.Domian;


namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class CreateShiftRequestDto
{



    public int? CenterId { get; set; }
    public int? DepartmantId { get; set; }
    public int DoctorId { get; set; }
    public string Description { get; set; }

    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public int MeetTime { get; set; }


    public ShiftActivityStatus ActivityStatus { get; set; }


    public ShiftType ShiftType { get; set; }
    public WeekDay DayOfWeek { get; set; }

}
