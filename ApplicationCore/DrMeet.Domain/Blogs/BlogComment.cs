using DrMeet.Domain.Base;
using DrMeet.Domain.Users;

namespace DrMeet.Domain.Blogs;

/// <summary>
/// مدل نظر ثبت‌شده برای یک مقاله.
/// این کلاس نمایانگر بازخورد یا دیدگاه کاربران درباره‌ی یک پست وبلاگ است.
/// </summary>
public class BlogComment : BaseEntityIdentity
{
   

    /// <summary>
    /// نام ثبت‌کننده نظر.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// موضوع یا تیتر نظر.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// ایمیل کاربر (اختیاری).
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// متن کامل نظر.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// امتیاز داده‌شده به مقاله (مثلاً از ۱ تا ۵).
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// شناسه مقاله .
    /// </summary>
    public required int BlogId { get; set; }

    /// <summary>
    /// شناسه کاربر ثبت‌کننده نظر.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// شیء مرتبط با کاربر نظر‌دهنده.
    /// </summary>
    public User User { get; set; } = new();
    public Blog? Blog { get; set; } 
}

