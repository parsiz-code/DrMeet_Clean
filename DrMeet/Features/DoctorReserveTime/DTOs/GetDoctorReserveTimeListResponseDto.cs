using DrMeet.Api.Shared.Domian;

using System.ComponentModel.DataAnnotations;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace DrMeet.Api.Features.DoctorReserveTimes.DTOs;

    public class GetDoctorReserveTimeListResponseDto
    {

    public int Id { get; set; }
           
    public int DoctorShiftId { get; set; }
           
    public int PatientId { get; set; }



}


public class GetFreeTimeDoctorReserveTimeListResponseDto
{

    public DateTime Date { get; set; }
    public int ShiftId { get; set; }
    public ShiftActivityStatus ActivityStatus { get; set; }
    public List<ShiftTimeItemDto> TurnTimes { get; set; }


}


public class GetFreeOneTimeDoctorReserveTimeListResponseDto
{
    public int ShiftId { get; set; } 

    public int TimeId { get; set; } 
    public string StartTime { get; set; } = string.Empty;
    public WeekDay DayOfWeek { get; set; } 
    public bool IsToday { get; set; }
    public DateTime Date { get; set; }
}
public class ShiftTimeItemDto
{
    public int TimeId { get; set; }
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public bool IsShiftAvailable { get; set; }
}
