namespace DrMeet.Domain.Enums;

/// <summary>
/// نوع مرکز درمانی.
/// این Enum برای دسته‌بندی مراکز به مطب، کلینیک یا درمانگاه استفاده می‌شود.
/// </summary>
public enum CenterOfType
{
    /// <summary>
    /// مطب شخصی پزشک.
    /// معمولاً با ظرفیت محدود و خدمات پایه.
    /// </summary>
    PrivateOffice = 0,

    /// <summary>
    /// کلینیک تخصصی یا عمومی.
    /// دارای چند پزشک و خدمات گسترده‌تر نسبت به مطب.
    /// </summary>
    Clinic = 1,

    /// <summary>
    /// درمانگاه شبانه‌روزی یا دولتی.
    /// ارائه خدمات اورژانسی و عمومی با ظرفیت بالا.
    /// </summary>
    Polyclinic = 2
}
