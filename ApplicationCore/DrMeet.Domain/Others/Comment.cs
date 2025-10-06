using DrMeet.Domain.Base;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Users;

namespace DrMeet.Domain.Others;

/// <summary>
/// مدل نظر یا دیدگاه ثبت‌شده توسط کاربران.
/// این کلاس نمایانگر یک کامنت شامل اطلاعات کاربر، موضوع، متن، امتیاز و ارتباط با موجودیت کاربری است.
/// </summary>
public class Comment : BaseEntityIdentity
{
    /// <summary>
    /// نام فردی که نظر را ثبت کرده است.
    /// این مقدار می‌تواند نام واقعی یا مستعار باشد.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// موضوع نظر یا عنوان مرتبط با محتوا.
    /// معمولاً برای دسته‌بندی یا نمایش خلاصه‌ای از نظر استفاده می‌شود.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// ایمیل ثبت‌کننده نظر.
    /// می‌تواند برای ارتباط یا اعتبارسنجی استفاده شود.
    /// </summary>
    public  string? Email { get; set; }

    /// <summary>
    /// متن کامل نظر یا دیدگاه ثبت‌شده توسط کاربر.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// امتیاز عددی داده‌شده توسط کاربر (مثلاً از 1 تا 5).
    /// این مقدار می‌تواند برای رتبه‌بندی یا تحلیل رضایت استفاده شود.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// شناسه کاربری که نظر را ثبت کرده است.
    /// این مقدار برای اتصال نظر به موجودیت <see cref="User"/> استفاده می‌شود.
    /// </summary>
    public int? UserId { get; set; }

    /// <summary>
    /// شیء کاربر مرتبط با این نظر.
    /// رابطه چند‌به‌یک با موجودیت <see cref="User"/>.
    /// </summary>
    public User User { get; set; } = new();

}
