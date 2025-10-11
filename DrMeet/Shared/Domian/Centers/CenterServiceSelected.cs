using DrMeet.Domain.Base;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگاشت بین مراکز درمانی و سرویس‌های قابل ارائه.
/// این کلاس نمایانگر رابطه بین یک مرکز درمانی و یک سرویس خاص است که توسط آن مرکز ارائه می‌شود.
/// </summary>
public class CenterServiceSelected : BaseEntityIdentity
{
    /// <summary>
    /// شناسه مرکز درمانی که این سرویس را ارائه می‌دهد.
    /// مقدار اختیاری است؛ در صورتی که هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شناسه سرویس یا خدمت قابل ارائه.
    /// مقدار اختیاری است؛ در صورتی که هنوز به سرویس خاصی نگاشت نشده باشد.
    /// </summary>
    public int? ServiceId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }

    /// <summary>
    /// شیء سرویس ارائه‌شده توسط تأمین‌کننده یا سیستم مرجع.
    /// رابطه چند‌به‌یک با موجودیت <see cref="ProviderServices"/>.
    /// </summary>
    public ProviderServices? ProviderServices { get; set; }
}
