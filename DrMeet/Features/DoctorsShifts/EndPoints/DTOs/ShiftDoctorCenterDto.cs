namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class ShiftDoctorCenterDto
{
    public int ShiftId { get; set; }
    public int DoctorId { get; set; }

    public string StartTime { get; set; }
    public string EndTime { get; set; }


    public ShiftType ShiftType { get; set; }
    public WeekDay DayOfWeek { get; set; }

}
