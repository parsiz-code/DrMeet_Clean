using DrMeet.Domain.Base;
using DrMeet.Domain.Doctors;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگاشت بین پزشک انتخاب‌شده در مرکز درمانی و خدماتی که ارائه می‌دهد.
/// این کلاس مشخص می‌کند که هر پزشک منتخب در مرکز، چه نوع خدمات درمانی یا تخصصی را ارائه می‌کند.
/// </summary>
public class CenterDoctorsServiceSelected : BaseEntityIdentity
{
    /// <summary>
    /// شناسه پزشک انتخاب‌شده در مرکز درمانی.
    /// مقدار اختیاری است؛ در صورتی که هنوز پزشک خاصی انتخاب نشده باشد.
    /// </summary>
    public int? CenterDoctorId { get; set; }

    /// <summary>
    /// شناسه خدمت درمانی یا تخصصی که توسط پزشک ارائه می‌شود.
    /// مقدار اختیاری است؛ در صورتی که هنوز خدمتی تعیین نشده باشد.
    /// </summary>
    public int? ProviderServiceId { get; set; }

    /// <summary>
    /// شیء پزشک منتخب در مرکز درمانی.
    /// رابطه چند‌به‌یک با موجودیت <see cref="CenterDoctorsSelected"/>.
    /// </summary>
    public CenterDoctorsSelected? Center { get; set; }

    /// <summary>
    /// شیء خدمت درمانی یا تخصصی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="ProviderServices"/>.
    /// </summary>
    public ProviderServices? ProviderService { get; set; }

    public ICollection<DoctorShiftService>? DoctorShiftServices { get; set; }
    public ICollection<DoctorReserveTime>? DoctorReserveTimes { get; set; }

}
