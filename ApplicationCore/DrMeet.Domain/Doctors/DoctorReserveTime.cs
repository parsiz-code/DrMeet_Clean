using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Enums;

namespace DrMeet.Domain.Doctors;

/// <summary>
/// مدل ثبت زمان رزرو شده توسط بیمار برای دریافت خدمت از پزشک.
/// این کلاس مشخص می‌کند که بیمار در چه تاریخی، برای چه خدمتی، و با چه وضعیتی رزرو انجام داده است.
/// </summary>
public class DoctorReserveTime : BaseEntityIdentity
{
    /// <summary>
    /// توضیحات تکمیلی درباره رزرو (مثلاً شرایط خاص، درخواست بیمار، یا یادداشت پزشک).
    /// این مقدار اختیاری است.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// شناسه زمان خاصی از شیفت پزشک که رزرو به آن تعلق دارد.
    /// این مقدار اختیاری است و می‌تواند به آیتم زمانی خاصی از شیفت متصل شود.
    /// </summary>
    public int? DoctorTimeId { get; set; }

    /// <summary>
    /// شناسه خدمت انتخاب‌شده توسط پزشک در مرکز درمانی.
    /// این مقدار الزامی است و به موجودیت <see cref="CenterDoctorsServiceSelected"/> متصل می‌شود.
    /// </summary>
    public required int CenterDoctorsServiceId { get; set; }

    /// <summary>
    /// شناسه بیمار که رزرو را انجام داده است.
    /// این مقدار به موجودیت <see cref="Patient"/> متصل می‌شود.
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// وضعیت رزرو (باز، تأیید شده، لغو شده و غیره).
    /// مقدار پیش‌فرض <see cref="ShiftStatus.Open"/> است.
    /// </summary>
    public ShiftStatus ShiftStatus { get; set; } = ShiftStatus.Open;

    /// <summary>
    /// تاریخ رزرو (روز مراجعه بیمار).
    /// این مقدار نشان‌دهنده تاریخ دقیق رزرو است.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// شیء بیمار مرتبط با رزرو.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Patient"/>.
    /// </summary>
    public DrMeet.Domain.Patient.Patient? Patient { get; set; }

    /// <summary>
    /// شیء خدمت انتخاب‌شده توسط پزشک در مرکز درمانی.
    /// رابطه چند‌به‌یک با موجودیت <see cref="CenterDoctorsServiceSelected"/>.
    /// </summary>
    public CenterDoctorsServiceSelected? CenterDoctorsServiceSelected { get; set; }
    public DoctorShiftTimeItem? DoctorShiftTimeItem { get; set; }
}
