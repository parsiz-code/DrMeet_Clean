using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگاشت پزشک انتخاب‌شده برای یک مرکز درمانی.
/// این کلاس مشخص می‌کند که کدام پزشک به‌صورت فعال یا منتخب در یک مرکز درمانی حضور دارد.
/// </summary>
public class CenterDoctorsSelected : BaseEntityIdentity
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
    public ICollection<CenterDoctorsDepartmantSelected>? CenterDoctorsDepartmant { get; set; } = [];
    public ICollection<CenterDoctorsServiceSelected>? CenterDoctorsService{ get; set; } = [];
    public ICollection<CenterDoctorServicePricing>? CenterDoctorServicePricing { get; set; } = [];
    public ICollection<CenterDoctorServiceOnlineConsultation>? CenterDoctorServiceOnlineConsultation { get; set; } = [];


}
