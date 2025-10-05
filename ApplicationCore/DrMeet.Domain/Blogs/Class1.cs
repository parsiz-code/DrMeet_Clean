using DrMeet.Domain.Base;
using DrMeet.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DrMeet.Domain.Blogs;
/// <summary>
/// مدل وبلاگ یا مقاله.
/// این کلاس نمایانگر یک مطلب منتشرشده توسط کاربر است که شامل عنوان، تصویر، متن و برچسب‌ها می‌باشد.
/// </summary>
public class Blog : BaseEntityIdentity
{
    /// <summary>
    /// عنوان مقاله یا پست وبلاگ.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// متن خلاصه یا معرفی کوتاه مقاله (اختیاری).
    /// </summary>
    public string? SummaryText { get; set; }

    /// <summary>
    /// مسیر تصویر اصلی مقاله.
    /// </summary>
    public required string ImagePath { get; set; }

    /// <summary>
    /// متن کامل مقاله یا پست (اختیاری).
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// برچسب‌های مرتبط با مقاله (اختیاری).
    /// </summary>
    public string? Tags { get; set; }

    /// <summary>
    /// شناسه کاربر نویسنده مقاله.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// شیء مرتبط با نویسنده مقاله.
    /// </summary>
    public virtual User User { get; set; } = new();

    /// <summary>
    /// لیست نظرات ثبت‌شده برای مقاله.
    /// </summary>
    public virtual List<BlogComment> Comments { get; set; } = [];
}


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

