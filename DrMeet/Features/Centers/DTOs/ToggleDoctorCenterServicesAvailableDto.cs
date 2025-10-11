using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class ToggleDoctorCenterServicesAvailableDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public required int CenterId { get; set; }
 
    [Required(ErrorMessage = "خدمت را انتخاب کنید")]
    public required int ServicesAvailableId { get; set; }

    [Required(ErrorMessage = "دکتر را انتخاب کنید")]
    public int? DoctorId { get; set; }
}
