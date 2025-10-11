using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نظر ثبت‌شده توسط کاربران برای یک مرکز درمانی.
/// این کلاس از کلاس پایه <see cref="Comment"/> ارث‌بری می‌کند و اطلاعات تکمیلی مربوط به مرکز درمانی را اضافه می‌کند.
/// </summary>
public class CenterComment : Comment
{
    /// <summary>
    /// شناسه مرکز درمانی که این نظر برای آن ثبت شده است.
    /// این مقدار الزامی است و برای اتصال نظر به مرکز درمانی استفاده می‌شود.
    /// </summary>
    public required int CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این نظر.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }
}
