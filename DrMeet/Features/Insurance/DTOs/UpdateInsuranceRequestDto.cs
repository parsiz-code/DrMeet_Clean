using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Insurances.DTOs;

public class UpdateInsuranceRequestDto
{

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int Id { get; set; }

    [Required(ErrorMessage = "بیمه  را انتخاب  کنید")]
    public string Name { get; set; }
    public bool Status { get; set; }
    [Required(ErrorMessage = "عکس را انتخاب  کنید")]
    public string Picture { get; set; }
    [Required(ErrorMessage = "وضییت  را انتخاب  کنید")]
    public bool IsBaseInsurance { get; set; }

}

