using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل بخش‌های داخلی یک مرکز درمانی.
/// این کلاس نمایانگر یک واحد یا دپارتمان خاص در یک مرکز درمانی است (مانند "رادیولوژی"، "اورژانس"، "کلینیک قلب").
/// </summary>
public class CenterDepartment : BaseEntityIdentity
{
    /// <summary>
    /// نام بخش یا دپارتمان (مثلاً "آزمایشگاه"، "پوست و مو").
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// شناسه مرکز درمانی مرتبط با این بخش.
    /// مقدار اختیاری است؛ در صورتی که بخش هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این بخش.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }
}
