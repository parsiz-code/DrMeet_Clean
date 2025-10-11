using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Enums;

namespace DrMeet.Domain.Doctors;

/// <summary>
/// مدل تعریف شیفت کاری پزشک در یک مرکز درمانی.
/// این کلاس اطلاعات مربوط به زمان‌بندی، نوع شیفت، وضعیت فعال بودن و میانگین زمان ویزیت را نگه‌داری می‌کند.
/// </summary>
public class DoctorShift : BaseEntityIdentity
{
    #region Properties



    /// <summary>
    /// شناسه مرکز درمانی که شیفت در آن تعریف شده است.
    /// این مقدار اختیاری است و در صورت وجود، به موجودیت <see cref="Center"/> متصل می‌شود.
    /// </summary>
    public int? CenterDoctorsDepartmantId { get; set; }

    /// <summary>
    /// توضیحات تکمیلی درباره شیفت (مثلاً نوع خدمت یا شرایط خاص).
    /// این مقدار اختیاری است.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// زمان شروع شیفت به‌صورت رشته (فرمت HH:mm).
    /// این مقدار الزامی است.
    /// </summary>
    public required string StartTime { get; set; }

    /// <summary>
    /// زمان پایان شیفت به‌صورت رشته (فرمت HH:mm).
    /// این مقدار الزامی است.
    /// </summary>
    public required string EndTime { get; set; }

    /// <summary>
    /// میانگین زمان ویزیت هر بیمار در دقیقه.
    /// این مقدار برای محاسبه ظرفیت شیفت استفاده می‌شود.
    /// </summary>
    public int MeetTime { get; set; }

    /// <summary>
    /// وضعیت فعال بودن شیفت.
    /// مشخص می‌کند که آیا شیفت در حال حاضر قابل استفاده است یا خیر.
    /// </summary>
    public ShiftActivityStatus ActivityStatus { get; set; }

    /// <summary>
    /// مدت زمان کل شیفت بر اساس اختلاف بین زمان شروع و پایان.
    /// به‌صورت خودکار محاسبه می‌شود.
    /// </summary>
    public TimeSpan Duration
        => TimeOnly.Parse(EndTime).ToTimeSpan() - TimeOnly.Parse(StartTime).ToTimeSpan();

    /// <summary>
    /// نوع شیفت (مثلاً صبح، عصر، شب).
    /// برای دسته‌بندی شیفت‌ها استفاده می‌شود.
    /// </summary>
    public ShiftType ShiftType { get; set; }

    /// <summary>
    /// روز هفته‌ای که شیفت در آن قرار دارد.
    /// از نوع Enum برای تعیین دقیق روز.
    /// </summary>
    public WeekDay DayOfWeek { get; set; }

    public virtual CenterDoctorsDepartmantSelected ?CenterDoctorsDepartmant { get; set; }
    public virtual ICollection<DoctorShiftTimeItem>? DoctorShiftTimeItems { get; set; }
    public virtual ICollection<DoctorShiftService>? DoctorShiftServices { get; set; }

    #endregion
}
