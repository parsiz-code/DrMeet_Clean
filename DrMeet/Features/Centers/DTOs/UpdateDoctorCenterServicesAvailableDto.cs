using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateDoctorCenterServicesAvailableDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public required int CenterId { get; set; }
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public required int DoctorId { get; set; }
    [Required(ErrorMessage = "خدمت را انتخاب کنید")]
    public List<int>? ServicesAvailableId { get; set; }
}
