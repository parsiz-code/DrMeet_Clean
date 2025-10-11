
using DrMeet.Api.Shared.Domian;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;

public class UpdateDoctorOnlineConsultationRequestDto
{

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int Id { get; set; }

    [Required(ErrorMessage = "دکتر را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "خدمات را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه خدمات باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int ServicesAvailableId { get; set; }
    [Required(ErrorMessage = "مبلغ  را وارد  کنید")]
    public decimal Price { get; set; }
}

