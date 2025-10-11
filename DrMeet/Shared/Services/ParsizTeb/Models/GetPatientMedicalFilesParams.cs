using System.ComponentModel.DataAnnotations;
using DrMeet.Api.Shared.PagedList;


namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class GetPatientMedicalFilesParams : PagedParamData
{
    
    [Required(ErrorMessage = "ارسال کد مرکز الزامی می باشد")]
    public string? CenterId { get; set; }
    
}