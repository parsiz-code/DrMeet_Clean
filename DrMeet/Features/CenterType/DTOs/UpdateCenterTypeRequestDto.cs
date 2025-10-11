using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.CenterTypes.DTOs;

public class UpdateCenterTypeRequestDto
{

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int Id { get; set; }

    [Required(ErrorMessage = "نوع مرکز  را انتخاب  کنید")]
    public string Name { get; set; }
    public bool Status { get; set; }

}

