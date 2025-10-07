using DrMeet.Domain.Base;
using DrMeet.Domain.Doctors;
using DrMeet.Domain.Others;
using DrMeet.Domain.Users;

namespace DrMeet.Domain.Patients;

/// <summary>
/// مدل نماینده‌ی بیمار در سیستم.
/// مشتق‌شده از کلاس پایه BaseEntityPerson و شامل اطلاعات کاربری، مرکز درمانی، بیمه و تصویر.
/// </summary>
public class Patient : BaseEntityPerson
{
    /// <summary>
    /// شناسه‌ی کاربری مرتبط با بیمار.
    /// این شناسه به موجودیت User اشاره دارد و برای ارتباط یک‌به‌یک استفاده می‌شود.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// شناسه‌ی بیمار در سیستم‌های خارجی یا ریموت (اختیاری).
    /// برای همگام‌سازی با سرویس‌های بیرونی کاربرد دارد.
    /// </summary>
    public int? PatientRemoteId { get; set; }

    /// <summary>
    /// شیء مرتبط با موجودیت User.
    /// این ویژگی برای دسترسی به اطلاعات کاربری بیمار استفاده می‌شود.
    /// </summary>
    public User User { get; set; }

    ///// <summary>
    ///// شناسه‌ی مرکز درمانی که بیمار در آن ثبت شده است (اختیاری).
    ///// می‌تواند برای فیلتر کردن بیماران بر اساس مراکز درمانی استفاده شود.
    ///// </summary>
    //public int? CenterId { get; set; }

    /// <summary>
    /// مسیر یا آدرس تصویر پروفایل بیمار.
    /// اگر مقداردهی نشده باشد، مقدار پیش‌فرض رشته‌ی خالی است.
    /// </summary>
    public string Picture { get; set; } = string.Empty;

    /// <summary>
    /// شناسه‌ی بیمه‌ی پایه‌ی بیمار (اختیاری).
    /// برای ثبت اطلاعات بیمه‌ی اصلی بیمار استفاده می‌شود.
    /// </summary>
    public int? InsuranceId { get; set; }

    /// <summary>
    /// شناسه‌ی بیمه‌ی تکمیلی بیمار (اختیاری).
    /// برای ثبت اطلاعات بیمه‌ی تکمیلی در صورت وجود استفاده می‌شود.
    /// </summary>
    public int? SupplementInsuranceId { get; set; }


    public Insurance? Insurance { get; set; }
    public Insurance? SupplementInsurance { get; set; }

    public ICollection<DoctorReserveTime>? DoctorReserveTimes { get; set; }
}
