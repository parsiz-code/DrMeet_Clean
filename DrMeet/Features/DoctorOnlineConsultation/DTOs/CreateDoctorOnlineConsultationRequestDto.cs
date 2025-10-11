using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;

public class CreateDoctorOnlineConsultationRequestDto
{
    [Required(ErrorMessage = "شناسه  دکتر را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "شناسه  خدمت را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه خدمات باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int ServicesAvailableId { get; set; }
    [Required(ErrorMessage = "مبلغ  را وارد  کنید")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "میزان درصد پراختی  را وارد  کنید")]
    public float PercentagePayment { get; set; }


}
