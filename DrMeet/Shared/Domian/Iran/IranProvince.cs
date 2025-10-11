using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Patients;

namespace DrMeet.Domain.Iran;


/// <summary>
/// مدل استان‌های ایران.
/// این کلاس نمایانگر یک استان در کشور ایران است و شامل لیستی از شهرهای زیرمجموعه آن می‌باشد.
/// </summary>
public class IranProvince : BaseEntityEmpty
{
    /// <summary>
    /// نام استان (مثلاً "تهران"، "اصفهان"، "فارس").
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// لیست شهرهای متعلق به این استان.
    /// رابطه یک‌به‌چند با موجودیت IranCity.
    /// </summary>
    public List<IranCity> Cities { get; set; } = [];

    public ICollection<Center>? Centers { get; set; }
    public ICollection<Patient>? Patients { get; set; }
    public ICollection<Doctor>? Doctors { get; set; }
}
