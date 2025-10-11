using DrMeet.Domain.Base;
using DrMeet.Domain.Doctors;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل نگاشت بین پزشک انتخاب‌شده در مرکز درمانی و بخش مربوطه.
/// این کلاس مشخص می‌کند که پزشک منتخب در کدام بخش از مرکز درمانی فعالیت دارد.
/// </summary>
public class CenterDoctorsDepartmantSelected : BaseEntityIdentity
{
    

    /// <summary>
    /// شناسه بخش درمانی که پزشک در آن فعالیت دارد.
    /// مقدار اختیاری است؛ در صورتی که هنوز بخشی تعیین نشده باشد.
    /// </summary>
    public int? CenterDepartmentId { get; set; }
    public int? CenterDoctorsSelectedId { get; set; }


    /// <summary>
    /// شیء بخش درمانی مرتبط با این نگاشت.
    /// رابطه چند‌به‌یک با موجودیت <see cref="CenterDepartment"/>.
    /// </summary>
    public CenterDepartment? CenterDepartment { get; set; }
    public CenterDoctorsSelected? CenterDoctorsSelected { get; set; }

    public virtual ICollection<DoctorShift> DoctorShift { get; set; } = [];
}
