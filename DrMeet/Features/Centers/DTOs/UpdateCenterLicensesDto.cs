using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateCenterLicensesDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }
    public List<int>? LicensesId { get; set; }
}
