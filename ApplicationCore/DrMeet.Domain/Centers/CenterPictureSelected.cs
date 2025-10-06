using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگاشت تصویر انتخاب‌شده برای یک مرکز درمانی.
/// این کلاس مشخص می‌کند که کدام تصویر از میان تصاویر بارگذاری‌شده به عنوان تصویر اصلی یا نمایشی مرکز انتخاب شده است.
/// </summary>
public class CenterPictureSelected : BaseEntityEmpty
{
    /// <summary>
    /// شناسه مرکز درمانی که تصویر برای آن انتخاب شده است.
    /// مقدار اختیاری است؛ در صورتی که هنوز به مرکز خاصی اختصاص داده نشده باشد.
    /// </summary>
    public int? CenterId { get; set; }

    /// <summary>
    /// شناسه تصویر انتخاب‌شده از میان تصاویر مرکز.
    /// مقدار اختیاری است؛ در صورتی که هنوز تصویری انتخاب نشده باشد.
    /// </summary>
    public int? CenterPictureId { get; set; }

    /// <summary>
    /// شیء مرکز درمانی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="Center"/>.
    /// </summary>
    public Center? Center { get; set; }

    /// <summary>
    /// شیء تصویر انتخاب‌شده از میان تصاویر مرکز.
    /// رابطه چند‌به‌یک با موجودیت <see cref="CenterPicture"/>.
    /// </summary>
    public CenterPicture? CenterPicture { get; set; }
}
