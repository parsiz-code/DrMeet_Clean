using DrMeet.Api.Shared.PagedList;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Licenses.DTOs;

public class GetLicensesResponseParams : PagedParamData
{
    public string? Title { get; set; }
    public bool Status { get; set; }
}
public class GetLicensesByCenterIdResponseParams : PagedParamData
{
    [Required(ErrorMessage = "لطفا 0 را وارد کنید")]
    [Display(Name = "مرکز")]
    public int CenterId { get; set; }

}