using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

public class GetShiftCenterRequestDto()
{
    [Required(ErrorMessage = " مرکز را وارد کنید")]
    public int CenterId { get; set; }

    [Required(ErrorMessage = " دکتر را وارد کنید")]
    public int DoctorId { get; set; }
    public WeekDay? DayOfWeek { get; set; }
}
