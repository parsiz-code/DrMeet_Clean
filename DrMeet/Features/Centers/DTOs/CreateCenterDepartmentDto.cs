using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class CreateCenterDepartmentDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }
    public List<string>? CenterDepartment { get; set; }
}
