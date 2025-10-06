using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMeet.Domain.Others;
/// <summary>
/// مدل خدمات قابل ارائه توسط ارائه‌دهندگان خدمات درمانی.
/// این کلاس نمایانگر نوع یا دسته‌ای از خدماتی است که توسط پزشکان یا مراکز درمانی ارائه می‌شود.
/// </summary>
public class ProviderServices:BaseEntityIdentity
{
    /// <summary>
    /// عنوان خدمت قابل ارائه (مثلاً "ویزیت حضوری"، "مشاوره تلفنی"، "آزمایش خون").
    /// این مقدار برای نمایش در رابط کاربری و دسته‌بندی خدمات استفاده می‌شود.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ترتیب نمایش خدمت در لیست‌ها یا فرم‌ها.
    /// مقدار عددی که می‌تواند برای مرتب‌سازی خدمات استفاده شود.
    /// </summary>
    public int Order { get; set; }

    public virtual ICollection<CenterServiceSelected> CenterServices { get; set; } = [];
    public virtual ICollection<CenterDoctorsServiceSelected> CenterDoctorsService { get; set; } = [];
    public virtual ICollection<CenterDoctorServicePricing> CenterDoctorPricing { get; set; } = [];
    public virtual ICollection<CenterDoctorServiceOnlineConsultation> CenterDoctorServiceOnlineConsultations { get; set; } = [];


}
