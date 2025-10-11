using DrMeet.Api.Shared.CustomAttribute;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Sliders.DTOs;


public class CreateSliderRequestDto
{
    [Required(ErrorMessage = "عنوان اسلایدر الزامی است")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "عنوان باید بین ۳ تا ۱۰۰ کاراکتر باشد")]
    public string Title { get; set; }

    //[Required(ErrorMessage = "شناسه دکتر الزامی است")]
    //public string DoctorId { get; set; }

    [Required(ErrorMessage = "تصویر اسلایدر الزامی است")]
    [MaxFileSize(200)] // محدود به ۲۰۰ کیلوبایت
    public string ImagePath { get; set; }

    //[Required(ErrorMessage = "تاریخ ایجاد الزامی است")]
    //[DataType(DataType.DateTime, ErrorMessage = "فرمت تاریخ معتبر نیست")]

}