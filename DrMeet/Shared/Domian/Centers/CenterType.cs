using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نوع مرکز درمانی.
/// این کلاس برای دسته‌بندی مراکز درمانی مانند کلینیک، بیمارستان، مطب و غیره استفاده می‌شود.
/// </summary>
public class CenterType : BaseEntitySoftEmpty
{
    /// <summary>
    /// عنوان نوع مرکز درمانی (مثلاً "کلینیک تخصصی"، "بیمارستان عمومی").
    /// این مقدار برای نمایش و فیلتر مراکز استفاده می‌شود.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// ترتیب نمایش نوع مرکز در لیست‌ها یا رابط کاربری.
    /// مقدار عددی که می‌تواند برای مرتب‌سازی استفاده شود.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// لیست مراکز درمانی مرتبط با این نوع.
    /// رابطه یک‌به‌چند بین CenterType و Center.
    /// </summary>
    public virtual ICollection<Center> Center { get; set; } = [];
}
