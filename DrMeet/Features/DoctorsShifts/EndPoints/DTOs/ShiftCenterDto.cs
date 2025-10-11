using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class ShiftCenterDto
{
    public GetDoctorDetailDto Doctor { get; set; }
    public string ShiftId { get; set; }
    public string DoctorId { get; set; }

    public string Description { get; set; }


    public string StartTime { get; set; }
    public string EndTime { get; set; }

    // میانگین زمان ویزیت
    public int MeetTime { get; set; }

    public ShiftActivityStatus ActivityStatus { get; set; }


    public ShiftType ShiftType { get; set; }
    public WeekDay DayOfWeek { get; set; }
}
