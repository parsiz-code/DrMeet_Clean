namespace DrMeet.Api.Shared.Domian;

using System.ComponentModel.DataAnnotations;

public class FileUploadSettingDto
{
    [Required(ErrorMessage = "نوع فایل الزامی است.")]
    public FileUploadSettingType Type { get; set; }

    [Range(1, long.MaxValue, ErrorMessage = "حداکثر حجم فایل باید بزرگ‌تر از صفر باشد.")]
    public long MaximumSize { get; set; }

    [Required(ErrorMessage = "نام نمایشی حجم فایل الزامی است.")]
    [MaxLength(50, ErrorMessage = "نام نمایشی حجم فایل نباید بیش از ۵۰ کاراکتر باشد.")]
    public string MaximumSizeFriendlyName { get; set; }

    [Required(ErrorMessage = "لیست پسوندهای معتبر الزامی است.")]
    [MinLength(1, ErrorMessage = "حداقل یک پسوند معتبر باید وارد شود.")]
    public List<string> ValidExtensions { get; set; }
}