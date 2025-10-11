using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorTariffs.DTOs;

public class CreateDoctorTariffRequestDto
{
    [Required(ErrorMessage = "دکتر  را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "مرکز را انتخاب  کنید")]
    public int CenterId { get; set; }
    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه خدمات باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int ServicesAvailableId { get; set; }
    [Required(ErrorMessage = "مبلغ  را وارد  کنید")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "میزان درصد پراختی  را وارد  کنید")]
    public float PercentagePayment { get; set; }
   

}
