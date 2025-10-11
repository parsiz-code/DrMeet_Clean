using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل تعریف مشاوره آنلاین پزشک برای یک خدمت خاص در یک مرکز درمانی.
/// این کلاس مشخص می‌کند که پزشک در کدام مرکز، چه خدمتی را به‌صورت آنلاین ارائه می‌دهد و تعرفه آن چقدر است.
/// </summary>
public class CenterDoctorServiceOnlineConsultation : BaseEntityIdentity
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
    /// شناسه خدمت درمانی یا تخصصی که مشاوره آنلاین برای آن تعریف شده است.
    /// این مقدار الزامی است و به موجودیت <see cref="ProviderServices"/> متصل می‌شود.
    /// </summary>
    public required int ServicesAvailableId { get; set; }

    /// <summary>
    /// شیء خدمت درمانی مرتبط با مشاوره آنلاین.
    /// رابطه چند‌به‌یک با موجودیت <see cref="ProviderServices"/>.
    /// </summary>
    public  ProviderServices? ProviderServices { get; set; }

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
