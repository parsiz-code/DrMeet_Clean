using DrMeet.Domain.Base;

namespace DrMeet.Domain.Iran;

/// <summary>
/// مدل شهرهای ایران.
/// این کلاس نمایانگر یک شهر در کشور ایران است که به یک استان تعلق دارد.
/// </summary>
public class IranCity : BaseEntityEmpty
{
    /// <summary>
    /// نام شهر (مثلاً "شیراز"، "مشهد"، "تبریز").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// شناسه استان مرتبط با این شهر.
    /// این مقدار برای ایجاد رابطه با IranProvince استفاده می‌شود.
    /// </summary>
    public int ProvinceId { get; set; }

    /// <summary>
    /// شیء استان مرتبط با این شهر.
    /// رابطه چند‌به‌یک با موجودیت IranProvince.
    /// </summary>
    public IranProvince? Province { get; set; }
}
