using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class GetShiftDoctorRequestDto()
{
    [Required(ErrorMessage = " دکتر را وارد کنید")]
    public string DoctorId { get; set; }
    public WeekDay? DayOfWeek { get; set; }
}
