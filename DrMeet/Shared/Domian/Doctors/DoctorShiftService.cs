using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;

namespace DrMeet.Domain.Doctors;

/// <summary>
/// مدل نگاشت بین شیفت پزشک و خدمات قابل ارائه در آن شیفت.
/// این کلاس مشخص می‌کند که در هر شیفت کاری پزشک، کدام خدمت درمانی یا تخصصی در مرکز مربوطه فعال است.
/// </summary>
public class DoctorShiftService : BaseEntityIdentity
{
    /// <summary>
    /// شناسه شیفت پزشک که خدمت در آن ارائه می‌شود.
    /// این مقدار به موجودیت <see cref="DoctorShift"/> متصل می‌شود.
    /// </summary>
    public int DoctorShiftId { get; set; }

    /// <summary>
    /// شیء شیفت پزشک مرتبط با خدمت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="DoctorShift"/>.
    /// </summary>
    public DoctorShift? DoctorShift { get; set; }

    /// <summary>
    /// شناسه خدمت انتخاب‌شده توسط پزشک در مرکز درمانی.
    /// این مقدار به موجودیت <see cref="CenterDoctorsServiceSelected"/> متصل می‌شود.
    /// </summary>
    public int CenterDoctorsServiceId { get; set; }

    /// <summary>
    /// شیء خدمت انتخاب‌شده توسط پزشک در مرکز درمانی.
    /// رابطه چند‌به‌یک با موجودیت <see cref="CenterDoctorsServiceSelected"/>.
    /// </summary>
    public CenterDoctorsServiceSelected? CenterDoctorsServiceSelected { get; set; }
}
