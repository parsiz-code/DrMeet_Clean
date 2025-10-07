using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Doctors;
using DrMeet.Domain.Users;

/// <summary>
/// مدل نماینده‌ی پزشک در سیستم، مشتق‌شده از BaseEntityPerson.
/// شامل اطلاعات تخصصی، رزومه، نوع مشاوره و قیمت‌ها.
/// </summary>
public class Doctor : BaseEntityPerson
{
    /// <summary>
    /// سازنده‌ی پیش‌فرض خصوصی برای جلوگیری از ایجاد نمونه بدون مقداردهی.
    /// </summary>
    public Doctor() { }

    /// <summary>
    /// شناسه‌ی پزشک در سیستم‌های خارجی یا ریموت.
    /// </summary>
    public int RemoteDoctorId { get; set; }

    /// <summary>
    /// تعداد سال‌های تجربه‌ی پزشک.
    /// </summary>
    public int ExperienceYears { get; set; } = 0;

    /// <summary>
    /// امتیاز پزشک بر اساس نظرات یا ارزیابی‌ها.
    /// </summary>
    public float Score { get; set; } = 0;

    /// <summary>
    /// آیا تصویر پزشک نمایش داده شود یا نه.
    /// </summary>
    public bool ShowPicture { get; set; } = true;

    /// <summary>
    /// مدت زمان ویزیت روزانه به دقیقه.
    /// </summary>
    public int DayVisitTime { get; set; } = 0;

    /// <summary>
    /// گروه تخصصی پزشک (مثلاً قلب، پوست، روان‌پزشکی).
    /// </summary>
    public string DoctorGroup { get; set; } = string.Empty;

    /// <summary>
    /// شناسه‌ی کاربری مرتبط با پزشک.
    /// </summary>
    public int UserId { get; set; } 

    /// <summary>
    /// بیوگرافی کوتاه پزشک.
    /// </summary>
    public string? Bio { get; set; } = string.Empty;

    /// <summary>
    /// توضیحات تکمیلی درباره‌ی پزشک.
    /// </summary>
    public string? Description { get; set; } = string.Empty;

  

    /// <summary>
    /// شماره نظام پزشکی پزشک.
    /// </summary>
    public string? NumberMedicalSystem { get; set; } = string.Empty;

    /// <summary>
    /// آیا پزشک در زمان‌های رزرو آنلاین نمایش داده شود؟
    /// </summary>
    public bool ShowInOnlineReserveTime { get; set; }

    /// <summary>
    /// آیا پزشک بیش از ۱۵ سال سابقه دارد؟ (غیر nullable)
    /// </summary>
    public bool OverFifteenYearsExperience { get; set; }

    #region UserInfo

    /// <summary>
    /// وب‌سایت شخصی پزشک.
    /// </summary>
    public string? WebSite { get; set; } = string.Empty;

    // اگر نیاز شد، می‌تونید آدرس رو هم اضافه کنید.
    public string? Address { get; set; } = string.Empty;

    #endregion


    /// <summary>
    /// آیا پزشک ویزیت حضوری انجام می‌دهد؟
    /// </summary>
    public bool InPerson { get; set; }

    /// <summary>
    /// هزینه‌ی ویزیت حضوری.
    /// </summary>
    public decimal PriceInPerson { get; set; } = 0;

    /// <summary>
    /// آیا پزشک مشاوره‌ی ویدیویی ارائه می‌دهد؟
    /// </summary>
    public bool IsVideoConsultation { get; set; }

    /// <summary>
    /// هزینه‌ی مشاوره‌ی ویدیویی.
    /// </summary>
    public decimal PriceIsVideoConsultation { get; set; } = 0;

    /// <summary>
    /// آیا پزشک مشاوره‌ی تلفنی ارائه می‌دهد؟
    /// </summary>
    public bool IsPhoneConsultation { get; set; }

    /// <summary>
    /// هزینه‌ی مشاوره‌ی تلفنی.
    /// </summary>
    public decimal PriceIsPhoneConsultation { get; set; } = 0;

    /// <summary>
    /// آیا پزشک مشاوره‌ی متنی ارائه می‌دهد؟
    /// </summary>
    public bool IsTextConsultation { get; set; }

    /// <summary>
    /// هزینه‌ی مشاوره‌ی متنی.
    /// </summary>
    public decimal PriceIsTextConsultation { get; set; } = 0;


    #region ارتباطات
    public virtual User User { get; set; }
    public virtual ICollection<CenterDoctorsSelected> CenterDoctors { get; set; }
    public virtual ICollection<DoctorComment> DoctorComments { get; set; }
    public virtual ICollection<DoctorExpertise> DoctorExpertises { get; set; }
    

    #endregion

}