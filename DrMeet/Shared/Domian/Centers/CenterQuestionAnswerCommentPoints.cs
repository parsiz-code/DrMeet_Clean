using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل امتیازدهی به پرسش یا پاسخ.
/// این کلاس نمایانگر یک نظر یا امتیاز مثبت یا منفی ثبت‌شده برای یک آیتم پرسش و پاسخ است.
/// </summary>
public class CenterQuestionAnswerCommentPoints : BaseEntityEmpty
{
    /// <summary>
    /// شناسه آیتم پرسش یا پاسخ که این امتیاز به آن تعلق دارد.
    /// </summary>
    public int CenterQuestionAnswerId { get; set; }

    /// <summary>
    /// متن نظر یا توضیحی که همراه با امتیاز ثبت شده است.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// مشخص‌کننده نوع امتیاز.
    /// اگر true باشد، امتیاز منفی است؛ در غیر این صورت امتیاز مثبت محسوب می‌شود.
    /// </summary>
    public bool IsNegativePoints { get; set; }

    public virtual CenterQuestionAnswer? CenterQuestionAnswer { get; set; }
}