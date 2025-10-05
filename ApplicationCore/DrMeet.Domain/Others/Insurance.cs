using DrMeet.Domain.Base;

namespace DrMeet.Domain.Others;

/// <summary>
/// مدل بیمه طرف قرارداد.
/// این کلاس اطلاعات مربوط به شرکت‌های بیمه‌ای را نگهداری می‌کند که با مراکز درمانی یا پزشکان همکاری دارند.
/// </summary>
public class Insurance : BaseEntityIdentity
{
    /// <summary>
    /// نام شرکت بیمه (مثلاً "تأمین اجتماعی"، "ایران"، "آتیه‌سازان").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// مسیر تصویر لوگو یا نماد بیمه.
    /// این مقدار می‌تواند شامل URL یا مسیر فایل باشد.
    /// </summary>
    public string? Picture { get; set; }

    /// <summary>
    /// ترتیب نمایش بیمه در لیست‌ها یا رابط کاربری.
    /// مقدار عددی که برای مرتب‌سازی استفاده می‌شود.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// نوع بیمه طرف قرارداد.
    /// اگر مقدار true باشد، بیمه پایه است؛ در غیر این صورت بیمه تکمیلی محسوب می‌شود.
    /// </summary>
    public bool IsBaseInsurance { get; set; }
}
