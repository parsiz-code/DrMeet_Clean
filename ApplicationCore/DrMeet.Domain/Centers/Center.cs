using DrMeet.Domain.Base;
using DrMeet.Domain.Enums;
using DrMeet.Domain.Users;

namespace DrMeet.Domain.Centers;

public class Center: BaseEntityIdentity
{

    /// <summary>
    /// شناسه مرکز در سیستم‌های خارجی یا ریموت.
    /// برای همگام‌سازی با سرویس‌های بیرونی کاربرد دارد.
    /// </summary>
    public int CenterRemoteId { get; set; } = 0;

    /// <summary>
    /// شناسه نوع مرکز درمانی (مثلاً کلینیک، بیمارستان، مطب).
    /// این مقدار می‌تواند به جدول نوع مراکز اشاره داشته باشد.
    /// </summary>
    public int CenterTypeId { get; set; }

    /// <summary>
    /// شناسه شهر محل فعالیت مرکز (اختیاری).
    /// برای دسته‌بندی جغرافیایی مراکز استفاده می‌شود.
    /// </summary>
    public int? CityId { get; set; }
    public CenterOfType? CenterOfType { get; set; }

    /// <summary>
    /// شناسه استان محل فعالیت مرکز (اختیاری).
    /// </summary>
    public int? ProvinceId { get; set; }

    /// <summary>
    /// منطقه یا ناحیه جغرافیایی مرکز (اختیاری).
    /// می‌تواند برای فیلترهای منطقه‌ای استفاده شود.
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// بیوگرافی یا معرفی کوتاه مرکز درمانی (اختیاری).
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// تاریخ تأسیس مرکز درمانی (اختیاری).
    /// برای نمایش سابقه فعالیت مرکز کاربرد دارد.
    /// </summary>
    public DateTime? DateOfEstablishment { get; set; }

    /// <summary>
    /// شماره تلفن مرکز درمانی (اختیاری).
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// شماره نمابر (فکس) مرکز درمانی (اختیاری).
    /// </summary>
    public string? FaxNumber { get; set; }

    /// <summary>
    /// آدرس وب‌سایت رسمی مرکز درمانی (اختیاری).
    /// </summary>
    public string? WebSite { get; set; }

    /// <summary>
    /// آدرس فیزیکی مرکز درمانی (اختیاری).
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// شناسه مجوزهای قانونی مرکز درمانی (اختیاری).
    /// می‌تواند به جدول مجوزها اشاره داشته باشد.
    /// </summary>
    public int? LicensesId { get; set; }

    /// <summary>
    /// توضیحات تکمیلی درباره‌ی مرکز درمانی (اختیاری).
    /// </summary>
    public string? Description { get; set; }



    /// <summary>
    /// شناسه کلینیک مرتبط با مرکز (اختیاری).
    /// در صورت وجود، مرکز می‌تواند زیرمجموعه‌ی یک کلینیک باشد.
    /// </summary>
    public int? ClinicId { get; set; }

    /// <summary>
    /// شناسه مطب مرتبط با مرکز (اختیاری).
    /// در صورت وجود، مرکز می‌تواند زیرمجموعه‌ی یک مطب باشد.
    /// </summary>
    public int? OfficeId { get; set; }


    /// <summary>
    /// تاریخ انقضای تعرفه‌های مرکز.
    /// برای مدیریت اعتبار تعرفه‌ها و تمدید آن‌ها استفاده می‌شود.
    /// </summary>
    public DateTime TariffExpirationDate { get; set; }



    /// <summary>
    /// لیست کاربران مرتبط با مرکز.
    /// این مجموعه می‌تواند شامل پزشکان، پرسنل یا مدیران باشد.
    /// </summary>
    public virtual ICollection<CenterPictureSelected> CenterPictureSelected { get; set; } = [];
    public virtual ICollection<CenterServiceSelected> CenterServices { get; set; } = [];
    public virtual ICollection<CenterDepartment> CenterDepartment { get; set; } = [];
    public virtual ICollection<CenterUserSelected> CenterUser { get; set; } = [];
    public virtual ICollection<CenterSocialMediaAccount> SocialMediaAccount { get; set; } = [];
    public virtual ICollection<CenterQuestionAnswer> CenterQuestionAnswer { get; set; } = [];
    public virtual ICollection<CenterInsurances> CenterInsurances { get; set; } = [];
    public virtual ICollection<CenterComment> CenterComment { get; set; } = [];
    public virtual ICollection<CenterDoctorsSelected> CenterDoctors { get; set; }


    public virtual CenterType? CenterType { get; set; } 

    ////خدمات قابل ارائه
    //public List<CenterService>? ServicesAvailableId { get; set; } = new();
    ////بیمه های پایه طرف قرارداد
    //public List<CenterInsurances>? ContractedBasicInsurancesId { get; set; } = new();
    ////بیمه های تکمیلی طرف قرارداد
    //public List<CenterInsurances>? ContractedSupplementalInsurancesId { get; set; } = new();
    //public List<CenterDepartment>? CenterDepartment { get; set; } = new();

    //public List<PictureCenter> Picture { get; set; } = [];
    //public List<Comment> Comment { get; set; } = [];
}
