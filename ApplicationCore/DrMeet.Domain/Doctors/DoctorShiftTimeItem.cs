using DrMeet.Domain.Base;

namespace DrMeet.Domain.Doctors;

/// <summary>
/// آیتم‌های زمانی مربوط به شیفت پزشک.
/// این کلاس بازه‌های زمانی داخل یک شیفت را مشخص می‌کند و نشان می‌دهد که آیا آن بازه برای ویزیت در دسترس است یا خیر.
/// </summary>
public class DoctorShiftTimeItem : BaseEntityEmpty
{
    /// <summary>
    /// زمان شروع بازه زمانی داخل شیفت.
    /// فرمت پیشنهادی HH:mm (مثلاً "08:30").
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// زمان پایان بازه زمانی داخل شیفت.
    /// فرمت پیشنهادی HH:mm (مثلاً "09:00").
    /// </summary>
    public string EndTime { get; set; } = string.Empty;

    /// <summary>
    /// وضعیت در دسترس بودن بازه زمانی برای ویزیت.
    /// اگر مقدار true باشد، این بازه قابل رزرو توسط بیمار است.
    /// </summary>
    public bool IsShiftAvailable { get; set; }

    /// <summary>
    /// شناسه شیفت اصلی که این آیتم به آن تعلق دارد.
    /// این مقدار به موجودیت <see cref="DoctorShift"/> متصل می‌شود.
    /// </summary>
    public int DoctorShiftId { get; set; }

    /// <summary>
    /// شیء شیفت پزشک مرتبط با این آیتم زمانی.
    /// رابطه چند‌به‌یک با موجودیت <see cref="DoctorShift"/>.
    /// </summary>
    public DoctorShift? DoctorShift { get; set; }

    public ICollection<DoctorReserveTime> DoctorReserveTimes { get; set; }
}



//public List<ShiftTimeItem> ShiftTime { get; set; } = [];
//public List<DoctorShiftService> ShiftServices { get; set; } = [];

//public List<ShiftTimeItem> ShiftTime { get; set; } = [];
//public List<DoctorShiftService> ShiftServices { get; set; } = [];