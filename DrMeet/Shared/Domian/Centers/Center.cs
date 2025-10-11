using DrMeet.Domain.Base;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Enums;
using DrMeet.Domain.Others;
using DrMeet.Domain.Users;
using NetTopologySuite.Geometries;
using SkiaSharp;

namespace DrMeet.Domain.Centers;

public class Center: BaseEntityLocation
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

    public CenterOfType? CenterOfType { get; set; }

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

    ///// <summary>
    ///// شناسه مجوزهای قانونی مرکز درمانی (اختیاری).
    ///// می‌تواند به جدول مجوزها اشاره داشته باشد.
    ///// </summary>
    //public int? LicensesId { get; set; }

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


    public string Picture { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// لیست کاربران مرتبط با مرکز.
    /// این مجموعه می‌تواند شامل پزشکان، پرسنل یا مدیران باشد.
    /// </summary>
    public virtual ICollection<CenterDoctorServiceOnlineConsultation> CenterDoctorServiceOnlineConsultation { get; set; } = [];
    public virtual ICollection<CenterDoctorServicePricing> CenterDoctorPricing { get; set; } = [];
    public virtual ICollection<CenterServiceSelected> CenterServices { get; set; } = [];
//    public virtual ICollection<CenterDoctorsDepartmantSelected> CenterDoctorsDepartmant { get; set; } = [];
    public virtual ICollection<CenterDepartment> CenterDepartment { get; set; } = [];
    public virtual ICollection<CenterUserSelected> CenterUser { get; set; } = [];
    public virtual ICollection<CenterSocialMediaAccount> SocialMediaAccount { get; set; } = [];
    public virtual ICollection<CenterQuestionAnswer> CenterQuestionAnswer { get; set; } = [];
    public virtual ICollection<CenterInsurances> CenterInsurances { get; set; } = [];
    public virtual ICollection<CenterComment> CenterComment { get; set; } = [];
    public virtual ICollection<CenterPicture>? CenterPicture { get; set; }
    public virtual ICollection<CenterDoctorsSelected>? CenterDoctors { get; set; }
    public virtual ICollection<CenterLicensesSelected>? CenterLicensesSelected { get; set; }
    public virtual ICollection<CenterDoctorsServiceSelected>? CenterDoctorsServiceSelected { get; set; }
    public virtual ICollection<Blog> Blogs { get; set; }
    public virtual ICollection<Slider> Sliders { get; set; }
    public virtual CenterType? CenterType { get; set; } 
    public virtual CenterLocation? CenterLocation { get; set; } 


}
public class CenterLocation:BaseEntityEmpty
{
    /// <summary>مختصات مکانی به‌صورت Point</summary>
    public Point? Location { get; set; }
    public string? Description { get; set; }

    public int CenterId { get; set; }

    public virtual Center Center { get; set; }
}