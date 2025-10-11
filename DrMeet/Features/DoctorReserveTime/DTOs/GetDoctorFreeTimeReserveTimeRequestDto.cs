using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.PagedList;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorReserveTimes.DTOs;

public class GetDoctorFreeTimeReserveTimeRequestDto 
{
    [Required(ErrorMessage = "دکتر را انتخاب کنید")]
    public int DoctorId { get; set; }
 
    public WeekDay? DayOfWeek { get; set; }
    public DateTime? Date { get; set; }
}
public class GetDoctorFreeOneTimeReserveTimeRequestDto
{
    [Required(ErrorMessage = "دکتر را انتخاب کنید")]
    public int DoctorId { get; set; } 

    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public required int CenterId { get; set; }
    public DateTime? Date { get; set; }
}