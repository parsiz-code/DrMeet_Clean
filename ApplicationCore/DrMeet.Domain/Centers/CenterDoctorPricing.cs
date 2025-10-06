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
    /// شناسه پزشک مرتبط با تعرفه.
    /// این مقدار الزامی است و به موجودیت <see cref="Doctor"/> متصل می‌شود.
    /// </summary>
    public required int DoctorId { get; set; }

    /// <summary>
    /// شناسه مرکز درمانی که تعرفه در آن تعریف شده است.
    /// این مقدار الزامی است و به موجودیت <see cref="Center"/> متصل می‌شود.
    /// </summary>
    public required int CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با تعرفه.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }

    /// <summary>
    /// شیء پزشک مرتبط با تعرفه.
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
/// <summary>
/// مدل تعریف مشاوره آنلاین پزشک برای یک خدمت خاص در یک مرکز درمانی.
/// این کلاس مشخص می‌کند که پزشک در کدام مرکز، چه خدمتی را به‌صورت آنلاین ارائه می‌دهد و تعرفه آن چقدر است.
/// </summary>
public class CenterDoctorServiceOnlineConsultation : BaseEntityIdentity
{
    /// <summary>
    /// شناسه مرکز درمانی که مشاوره آنلاین در آن ارائه می‌شود.
    /// این مقدار الزامی است و به موجودیت <see cref="Center"/> متصل می‌شود.
    /// </summary>
    public required int CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با مشاوره آنلاین.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }

    /// <summary>
    /// شناسه پزشک ارائه‌دهنده مشاوره آنلاین.
    /// این مقدار الزامی است و به موجودیت <see cref="Doctor"/> متصل می‌شود.
    /// </summary>
    public required int DoctorId { get; set; }

    /// <summary>
    /// شیء پزشک مرتبط با مشاوره آنلاین.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Doctor"/>.
    /// </summary>
    public required Doctor Doctor { get; set; }

    /// <summary>
    /// شناسه خدمت درمانی یا تخصصی که مشاوره آنلاین برای آن تعریف شده است.
    /// این مقدار الزامی است و به موجودیت <see cref="ProviderServices"/> متصل می‌شود.
    /// </summary>
    public required int ServicesAvailableId { get; set; }

    /// <summary>
    /// شیء خدمت درمانی مرتبط با مشاوره آنلاین.
    /// رابطه چند‌به‌یک با موجودیت <see cref="ProviderServices"/>.
    /// </summary>
    public required ProviderServices ProviderServices { get; set; }

    /// <summary>
    /// مبلغ تعرفه مشاوره آنلاین برای خدمت مورد نظر.
    /// این مقدار نشان‌دهنده هزینه نهایی مشاوره برای بیمار است.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// درصد پرداختی به پزشک از مبلغ مشاوره.
    /// این مقدار برای محاسبه سهم پزشک از درآمد مشاوره آنلاین استفاده می‌شود.
    /// </summary>
    public float PercentagePayment { get; set; }
}
