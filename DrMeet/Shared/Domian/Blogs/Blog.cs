using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
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
    /// تاریخ ایجاد
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;

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
    public required int CenterId { get; set; }

    /// <summary>
    /// شیء مرتبط با نویسنده مقاله.
    /// </summary>
    public virtual Center Center { get; set; } = new();

    /// <summary>
    /// لیست نظرات ثبت‌شده برای مقاله.
    /// </summary>
    public virtual List<BlogComment> Comments { get; set; } = [];
}

