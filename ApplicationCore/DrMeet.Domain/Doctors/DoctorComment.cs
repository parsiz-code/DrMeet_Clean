using DrMeet.Domain.Others;

namespace DrMeet.Domain.Doctors;


/// <summary>
/// مدل نظر ثبت‌شده توسط کاربران برای یک پزشک.
/// این کلاس از <see cref="Comment"/> ارث‌بری می‌کند و شامل امتیازهای جزئی‌تری برای ارزیابی عملکرد پزشک است.
/// </summary>
public class DoctorComment : Comment
{
    /// <summary>
    /// امتیاز برخورد مناسب پزشک با بیمار.
    /// معمولاً نشان‌دهنده رفتار حرفه‌ای، احترام و ارتباط مؤثر است.
    /// </summary>
    public int BehaviorScore { get; set; }

    /// <summary>
    /// امتیاز کیفیت درمان ارائه‌شده توسط پزشک.
    /// شامل دقت در تشخیص، اثربخشی درمان و رضایت بیمار از روند درمان.
    /// </summary>
    public int TreatmentQualityScore { get; set; }

    /// <summary>
    /// امتیاز صرفه اقتصادی درمان.
    /// نشان‌دهنده تناسب هزینه‌ها با کیفیت خدمات و پرهیز از هزینه‌های غیرضروری.
    /// </summary>
    public int EconomicEfficiencyScore { get; set; }

    /// <summary>
    /// امتیاز بهبودی بیمار پس از درمان توسط پزشک.
    /// می‌تواند بر اساس تجربه شخصی یا نتایج بالینی ثبت شود.
    /// </summary>
    public int RecoveryScore { get; set; }

    /// <summary>
    /// شناسه پزشک مرتبط با این نظر.
    /// این مقدار الزامی است و برای اتصال نظر به موجودیت <see cref="Doctor"/> استفاده می‌شود.
    /// </summary>
    public required int DoctorId { get; set; }

    /// <summary>
    /// شیء پزشک مرتبط با این نظر.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Doctor"/>.
    /// </summary>
    public Doctor? Doctor { get; set; }
}
