using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل تعرفه خدمات پزشک در یک مرکز درمانی.
/// این کلاس مشخص می‌کند که پزشک برای ارائه یک خدمت خاص در یک مرکز مشخص، چه مبلغی دریافت می‌کند و چه درصدی از پرداخت به او تعلق می‌گیرد.
/// </summary>
public class CenterDoctorServicePricing : BaseEntityIdentity
{
    /// <summary>
    /// شناسه مرکز درمانی که پزشک برای آن انتخاب شده است.
    /// مقدار اختیاری است؛ در صورتی که هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شناسه پزشک انتخاب‌شده برای مرکز درمانی.
    /// مقدار اختیاری است؛ در صورتی که هنوز پزشک خاصی انتخاب نشده باشد.
    /// </summary>
    public int? DoctorId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }

    /// <summary>
    /// شیء پزشک مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Doctor"/>.
    /// </summary>
    public Doctor? Doctor { get; set; }

    /// <summary>
    /// شناسه خدمت درمانی یا تخصصی که تعرفه برای آن تعریف شده است.
    /// این مقدار الزامی است و به موجودیت <see cref="ProviderServices"/> متصل می‌شود.
    /// </summary>
    public required int ProviderServicesId { get; set; }

    /// <summary>
    /// شیء خدمت درمانی مرتبط با تعرفه.
    /// رابطه چند‌به‌یک با موجودیت <see cref="ProviderServices"/>.
    /// </summary>
    public ProviderServices? ProviderServices { get; set; }

    /// <summary>
    /// مبلغ تعرفه برای خدمت مورد نظر.
    /// این مقدار نشان‌دهنده هزینه نهایی خدمت برای بیمار است.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// درصد پرداختی به پزشک از مبلغ تعرفه.
    /// این مقدار می‌تواند برای محاسبه سهم پزشک از درآمد خدمت استفاده شود.
    /// </summary>
    public float PercentagePayment { get; set; }
}
