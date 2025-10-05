using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل بیمه‌های طرف قرارداد با یک مرکز درمانی.
/// این کلاس نمایانگر یک نوع بیمه (مانند "تأمین اجتماعی"، "سلامت"، "آتیه‌سازان") است که توسط مرکز درمانی پشتیبانی می‌شود.
/// </summary>
public class CenterInsurances : BaseEntityIdentity
{
    /// <summary>
    /// نام بیمه طرف قرارداد (مثلاً "تأمین اجتماعی" یا "بیمه ایران").
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// شناسه مرکز درمانی مرتبط با این بیمه.
    /// مقدار اختیاری است؛ در صورتی که بیمه هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این بیمه.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }
}
