using DrMeet.Domain.Base;
using DrMeet.Domain.Enums;

namespace DrMeet.Domain.ApplicationSettings;

/// <summary>
/// تنظیمات مربوط به آپلود فایل.
/// این کلاس مشخص می‌کند که چه نوع فایلی قابل آپلود است، با چه محدودیتی در حجم و پسوند.
/// </summary>
public class ApplicationSettingFileUpload : BaseEntityIdentity
{
    /// <summary>
    /// نوع فایل قابل آپلود (مثلاً تصویر، ویدیو، سند).
    /// مقدار از نوع Enum تعریف‌شده به نام <see cref="FileUploadSettingType"/>.
    /// </summary>
    public FileUploadSettingType Type { get; set; }

    /// <summary>
    /// حداکثر حجم مجاز برای آپلود فایل (بر حسب بایت).
    /// </summary>
    public long MaximumSize { get; set; }

    /// <summary>
    /// نمایش کاربرپسند حجم مجاز (مثلاً "5MB").
    /// مقدار اختیاری برای نمایش در رابط کاربری.
    /// </summary>
    public string? MaximumSizeFriendlyName { get; set; }

    /// <summary>
    /// شناسه تنظیمات اپلیکیشن مرتبط با این تنظیم آپلود.
    /// </summary>
    public int ApplicationSettingId { get; set; }

    /// <summary>
    /// شیء تنظیمات اپلیکیشن مرتبط.
    /// رابطه چند‌به‌یک با <see cref="ApplicationSetting"/>.
    /// </summary>
    public ApplicationSetting? ApplicationSetting { get; set; }

    /// <summary>
    /// لیست پسوندهای مجاز برای آپلود فایل (مثلاً ".jpg", ".pdf").
    /// مقدار اختیاری برای محدودسازی نوع فایل‌ها.
    /// </summary>
    public List<string>? ValidExtensions { get; set; }
}
