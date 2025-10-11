using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class ToggleCenterServicesAvailableDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public required int CenterId { get; set; }
    [Required(ErrorMessage = "سرویس را انتخاب کنید")]
    public required int ServicesAvailableId { get; set; }



}
