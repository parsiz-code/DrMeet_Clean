namespace DrMeet.Api.Shared.Domian;


using System.ComponentModel.DataAnnotations;

public class ApplicationSettingResponseDto
{
    public  string? AppVersion { get; set; }
    public  string? ApiVersion { get; set; }

    public List<FileUploadSettingDto> FileUploadSetting { get; set; } = [];
}
public class ApplicationSettingRequestDto
{
    [Required(ErrorMessage = "نسخه‌ی اپلیکیشن الزامی است.")]
    [MaxLength(20, ErrorMessage = "نسخه‌ی اپلیکیشن نباید بیش از ۲۰ کاراکتر باشد.")]
    public string AppVersion { get; set; }

    [Required(ErrorMessage = "نسخه‌ی API الزامی است.")]
    [MaxLength(20, ErrorMessage = "نسخه‌ی API نباید بیش از ۲۰ کاراکتر باشد.")]
    public required string ApiVersion { get; set; }

    [Required(ErrorMessage = "تنظیمات آپلود فایل الزامی است.")]
    [MinLength(1, ErrorMessage = "حداقل یک تنظیم آپلود فایل باید وارد شود.")]
    public List<FileUploadSettingDto> FileUploadSetting { get; set; } = [];
}
