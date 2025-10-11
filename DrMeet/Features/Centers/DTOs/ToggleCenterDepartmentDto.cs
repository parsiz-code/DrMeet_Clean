using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class ToggleCenterDepartmentDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }
    [Required(ErrorMessage = "بخش  را انتخاب کنید")]
    public int DepartmentId { get; set; }
}
