using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;
/// <summary>
/// مدل نگاشت بیمه‌های طرف قرارداد با مراکز درمانی.
/// این کلاس مشخص می‌کند که کدام بیمه توسط کدام مرکز درمانی پشتیبانی می‌شود.
/// </summary>
public class CenterInsurances : BaseEntityIdentity
{
    /// <summary>
    /// شیء بیمه مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Insurance"/>.
    /// </summary>
    public Insurance? Insurance { get; set; }

    /// <summary>
    /// شناسه بیمه‌ای که توسط مرکز درمانی پشتیبانی می‌شود.
    /// مقدار اختیاری است؛ در صورتی که هنوز بیمه‌ای انتخاب نشده باشد.
    /// </summary>
    public int? InsuranceId { get; set; }

    /// <summary>
    /// شناسه مرکز درمانی که این بیمه را پشتیبانی می‌کند.
    /// مقدار اختیاری است؛ در صورتی که هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }
}
