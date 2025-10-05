using DrMeet.Domain.Base;

namespace DrMeet.Domain.ApplicationSettings;

/// <summary>
/// تنظیمات کلی مربوط به نسخه‌های اپلیکیشن.
/// این کلاس شامل اطلاعاتی درباره نسخه فعلی اپلیکیشن و API و همچنین تنظیمات آپلود فایل‌ها می‌باشد.
/// </summary>
public class ApplicationSetting : BaseEntityIdentity
{
    /// <summary>
    /// نسخه فعلی اپلیکیشن (مثلاً "1.0.0").
    /// </summary>
    public string? AppVersion { get; set; }

    /// <summary>
    /// نسخه API مورد استفاده (مثلاً "v1" یا "v2").
    /// </summary>
    public string? ApiVersion { get; set; }

    /// <summary>
    /// لیست تنظیمات مربوط به آپلود فایل‌ها.
    /// هر تنظیم می‌تواند نوع خاصی از فایل را با محدودیت‌های خاص مدیریت کند.
    /// </summary>
    public virtual List<ApplicationSettingFileUpload>? FileUploadSetting { get; set; }
}
