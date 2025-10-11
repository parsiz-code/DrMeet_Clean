using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMeet.Domain.Others;

/// <summary>
/// مدل اسلایدر برای نمایش در صفحات وب یا اپلیکیشن.
/// این کلاس نمایانگر یک آیتم تصویری است که معمولاً در بخش‌های تبلیغاتی، معرفی یا اطلاع‌رسانی استفاده می‌شود.
/// </summary>
public class Slider : BaseEntityIdentity
{  
    
    /// <summary>
     /// تاریخ ایجاد
     /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;
    /// <summary>
    /// عنوان یا تیتر اسلایدر.
    /// این مقدار برای نمایش متنی در کنار تصویر استفاده می‌شود.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// مسیر یا آدرس تصویر اسلایدر.
    /// این مقدار می‌تواند شامل URL یا مسیر فایل باشد که تصویر مربوطه را نمایش می‌دهد.
    /// </summary>
    public string ImagePath { get; set; }

    /// <summary>
    /// شناسه کاربری ایجادکننده یا مالک اسلایدر.
    /// این مقدار برای ارتباط با موجودیت User استفاده می‌شود.
    /// </summary>
    public int? CenterId { get; set; }
    public Center Center { get; set; } = new();


}
/// <summary>
/// مدل روزهای تعطیل.
/// این کلاس نمایانگر یک روز تعطیل رسمی یا مناسبتی است که می‌تواند در تقویم مرکز درمانی یا سیستم نوبت‌دهی لحاظ شود.
/// </summary>
public class Holidays : BaseEntityIdentity
{

    /// <summary>
    /// توضیح یا عنوان مناسبت تعطیل (مثلاً "تعطیلی به مناسبت عید فطر" یا "تعطیلی رسمی").
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// تاریخ دقیق تعطیلی.
    /// از نوع DateOnly برای ذخیره فقط بخش تاریخی بدون زمان.
    /// </summary>
    public DateOnly Date { get; set; }


}
