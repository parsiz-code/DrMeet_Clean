using DrMeet.Domain.Base;
using DrMeet.Domain.Users;
using System.Xml.Linq;

namespace DrMeet.Domain.Centers;

/// <summary>
/// مدل واسط برای ارتباط بین کاربران و مراکز درمانی.
/// این کلاس نمایانگر رابطه‌ی چند-به-چند بین موجودیت‌های User و Center است.
/// </summary>
public class CenterUserSelected : BaseEntityEmpty
{
    /// <summary>
    /// شناسه‌ی کاربر مرتبط با مرکز درمانی.
    /// این شناسه به موجودیت User اشاره دارد.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// شیء مرتبط با موجودیت User.
    /// اطلاعات کامل کاربر از طریق این ویژگی قابل دسترسی است.
    /// </summary>
    public virtual User User { get; set; } = new User();

    /// <summary>
    /// شناسه‌ی مرکز درمانی مرتبط با کاربر.
    /// این شناسه به موجودیت Center اشاره دارد.
    /// </summary>
    public int CenterId { get; set; }

    /// <summary>
    /// شیء مرتبط با موجودیت Center.
    /// اطلاعات کامل مرکز درمانی از طریق این ویژگی قابل دسترسی است.
    /// </summary>
    public virtual Center Center { get; set; } = new Center();
}
