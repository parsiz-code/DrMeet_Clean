using DrMeet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrMeet.Domain.Base;
public class BaseEntityEmpty 
{
    public int Id { get; set; }


}
public class BaseEntityIdentity: BaseEntity
{
    public int Id { get; set; }


}
public class BaseEntity
{

    public bool Deleted { get; set; } = false;

    /// <summary>
    /// تاریخ ایجاد
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;

    /// <summary>
    /// تاریخ بروزرسانی
    /// </summary>
    public DateTime UpdateDate { get; set; } = DateTime.Now;

    /// <summary>
    /// وضعیت
    /// </summary>
    public bool Status { get; set; } = true;
}
public class BaseEntityPerson: BaseEntityIdentity
{
    public GenderModel? Gender { get; set; }
    public string? FatherName { get; set; } = string.Empty;
    public string? BirthDate { get; set; }
    public string? NationalCode { get; set; } = string.Empty;
    public string? ProvinceId { get; set; } = string.Empty;
    public string? CityId { get; set; } = string.Empty;
    public string? Region { get; set; } = string.Empty;
}
