using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل پرسش و پاسخ مربوط به یک مرکز درمانی.
/// این کلاس نمایانگر یک سؤال یا پاسخ ثبت‌شده توسط کاربران یا مدیران مرکز است.
/// می‌تواند شامل پاسخ‌های تو در تو (از طریق ParentId) و امتیازهای مثبت و منفی باشد.
/// </summary>
public class CenterQuestionAnswer : BaseEntityIdentity
{


    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;


    /// <summary>
    /// شناسه مرکز درمانی مرتبط با این پرسش یا پاسخ.
    /// </summary>
    public required int CenterId { get; set; }

    /// <summary>
    /// شناسه والد (در صورت وجود)، برای پشتیبانی از ساختار سلسله‌مراتبی پرسش و پاسخ.
    /// اگر مقدار داشته باشد، این آیتم زیرمجموعه یک سؤال یا پاسخ دیگر است.
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// متن کامل سؤال یا پاسخ.
    /// </summary>
    public required string Text { get; set; }

    /// <summary>
    /// نوع آیتم (مثلاً "سؤال"، "پاسخ"، "نظر").
    /// مقدار اختیاری برای دسته‌بندی.
    /// </summary>
    public string? Type { get; set; }



    /// <summary>
    /// شیء مرکز درمانی مرتبط با این آیتم.
    /// رابطه چند‌به‌یک با Center.
    /// </summary>
    public virtual Center? Center { get; set; }

    /// <summary>
    /// لیست امتیازهای مثبت ثبت‌شده برای این آیتم.
    /// </summary>
    public List<CenterQuestionAnswerCommentPoints> PositivePoints { get; set; } = [];

    /// <summary>
    /// لیست امتیازهای منفی ثبت‌شده برای این آیتم.
    /// </summary>
    public List<CenterQuestionAnswerCommentPoints> NegativePoints { get; set; } = [];
}
