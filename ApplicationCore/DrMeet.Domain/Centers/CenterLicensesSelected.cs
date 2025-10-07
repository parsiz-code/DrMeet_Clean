using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگهداری مجوزهای انتخاب‌شده توسط مراکز درمانی.
/// این کلاس مشخص می‌کند که یک مرکز درمانی کدام مجوز قانونی یا تخصصی را دریافت یا ثبت کرده است.
/// </summary>
public class CenterLicensesSelected : BaseEntityEmpty
{
    /// <summary>
    /// شناسه مرکز درمانی که مجوز برای آن ثبت شده است.
    /// این مقدار اختیاری است و به موجودیت <see cref="Center"/> متصل می‌شود.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شناسه مجوز انتخاب‌شده توسط مرکز درمانی.
    /// این مقدار اختیاری است و به موجودیت <see cref="Licenses"/> متصل می‌شود.
    /// </summary>
    public int? LicensesId { get; set; }

    /// <summary>
    /// شیء مجوز مرتبط با مرکز درمانی.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Licenses"/>.
    /// </summary>
    public Licenses? Licenses { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با مجوز.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }
}
