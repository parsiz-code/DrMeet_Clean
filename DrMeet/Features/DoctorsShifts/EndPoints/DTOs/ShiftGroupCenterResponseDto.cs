namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class ShiftGroupCenterResponseDto
{
    public WeekDay? DayOfWeek { get; set; }

    public List<ShiftCenterDto> Shifts { get; set; }
}