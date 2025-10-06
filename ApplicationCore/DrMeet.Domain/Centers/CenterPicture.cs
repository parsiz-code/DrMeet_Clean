using DrMeet.Domain.Base;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل تصویرهای بارگذاری‌شده برای مراکز درمانی.
/// این کلاس نمایانگر یک تصویر خاص از مرکز درمانی است که می‌تواند شامل عکس محیط، لوگو، یا مدارک باشد.
/// </summary>
public class CenterPicture : BaseEntityEmpty
{
    /// <summary>
    /// مسیر یا آدرس فایل تصویر ذخیره‌شده (مثلاً URL یا نام فایل در سرور).
    /// این مقدار نباید خالی باشد و باید به یک فایل معتبر اشاره کند.
    /// </summary>
    public string Picture { get; set; } = string.Empty;

    /// <summary>
    /// نوع تصویر (مثلاً "لوگو"، "محیط"، "مدرک").
    /// این مقدار برای دسته‌بندی تصاویر استفاده می‌شود و می‌تواند در فیلتر یا نمایش هدفمند کاربرد داشته باشد.
    /// </summary>
    public string PictureType { get; set; } = string.Empty;

    public virtual ICollection<CenterPictureSelected> CenterPictureSelected { get; set; } = [];

}
