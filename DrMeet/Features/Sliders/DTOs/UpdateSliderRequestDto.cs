using DrMeet.Api.Shared.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Sliders.DTOs;



public class UpdateSliderRequestDto
{
    [Required(ErrorMessage = "شناسه اسلایدر الزامی است")]
    public int Id { get; set; }

    [Required(ErrorMessage = "عنوان اسلایدر الزامی است")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "عنوان باید بین ۳ تا ۱۰۰ کاراکتر باشد")]
    public string Title { get; set; }

    //[Required(ErrorMessage = "شناسه دکتر الزامی است")]
    //public string DoctorId { get; set; }

    [Required(ErrorMessage = "تصویر اسلایدر الزامی است")]
 
    [MaxFileSize(200)] // محدود به ۲۰۰ کیلوبایت
    public string ImagePath { get; set; } // اختیاری در به‌روزرسانی
}