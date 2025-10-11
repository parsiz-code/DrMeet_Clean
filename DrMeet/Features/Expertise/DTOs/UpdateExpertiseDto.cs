using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Expertises.DTOs;

public class UpdateExpertiseDto
{

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int Id { get; set; }

    [Required(ErrorMessage = "نام تخصص را انتخاب  کنید")]
    public string Name { get; set; }

}

