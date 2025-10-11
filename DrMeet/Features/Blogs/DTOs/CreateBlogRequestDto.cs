using DrMeet.Api.Shared.CustomAttribute;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Blogs.DTOs;

public class CreateBlogRequestDto
{
    [Required(ErrorMessage = "توضیحات را وارد کنید")]
    public string? SummaryText { get; set; }
    [Required(ErrorMessage ="عنوان را وارد کنید")]
    public string Title { get; set; }
    [Required(ErrorMessage ="متن مقاله را و.ارد کنید")]
    public string Text { get; set; }
    //[Required(ErrorMessage = "عکس مقاله را وارد کنید")]
    //[MaxFileSize(200)] // محدود به ۲۰۰ کیلوبایت
    public string? ImagePath { get; set; }
    [Required(ErrorMessage = "تگ ها را وارد کنید")]
    public List<string> Tags { get; set; }
 //   [Required(ErrorMessage = "دکتر مورد نظر را انتخاب کنید")]
    //public string DoctorId { get; set; }
    //[Required(ErrorMessage = "نام نویسنده مقاله را انتخاب کنید")]
    //public string AuthorName { get; set; }
    //[Required(ErrorMessage = "عکس نویسنده را انتخاب کنید")]
    //public IFormFile? AuthorImagePath { get; set; }
    //[Required(ErrorMessage = "تاریخ را وارد کنید")]
    //public DateTime CreateDate { get; set; }
}
