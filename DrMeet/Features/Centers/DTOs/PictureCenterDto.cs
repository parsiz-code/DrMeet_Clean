using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class PictureCenterDto
{
    [Required(ErrorMessage = "عکس را انتخاب کنید")]
    public string Picture { get; set; }

    [Required(ErrorMessage = "نوع را انتخاب کنید")]
    public string PictureType { get; set; }
}
