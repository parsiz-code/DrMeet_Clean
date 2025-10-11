namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class ShiftGroupResponseDto
{
    public WeekDay? DayOfWeek { get; set; }

    public List<ShiftResposneDto> Shifts { get; set; }
}
