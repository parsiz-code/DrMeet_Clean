using DrMeet.Domain.Base;

namespace DrMeet.Domain.Others;

/// <summary>
/// مدل تخصص یا حوزه کاری.
/// این کلاس نمایانگر یک تخصص پزشکی یا درمانی است که می‌تواند به پزشکان یا مراکز درمانی اختصاص داده شود.
/// </summary>
public class Expertise : BaseEntityIdentity
{
    /// <summary>
    /// عنوان تخصص (مثلاً "قلب و عروق"، "پوست و مو"، "روان‌پزشکی").
    /// این مقدار برای نمایش در رابط کاربری و دسته‌بندی تخصص‌ها استفاده می‌شود.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
