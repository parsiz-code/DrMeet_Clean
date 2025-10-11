using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateCenterPictureDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; } = 0;
    [Required(ErrorMessage = " عکس اصلی را انتخاب کنید")]
    public string MainPicture { get; set; } = string.Empty;

    public List<PictureCenterDto> Picture { get; set; } = [];
}
