using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DrMeet.Domain.Users;

using DrMeet.Domain.Base;
using DrMeet.Domain.Blogs;
using DrMeet.Domain.Centers;
using DrMeet.Domain.Others;
using DrMeet.Domain.Patient;
using ErrorOr;


/// <summary>
/// مدل کاربر سیستم
/// </summary>
public  class User : BaseEntityIdentity
{
    // سازنده خصوصی برای جلوگیری از ایجاد نمونه بدون مقداردهی
    public User() { }



    /// <summary>
    /// نام و نام  خانوادگی 
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// رمز عبور
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// شماره موبایل
    /// </summary>
    public string? MobileNumber { get; set; }


    /// <summary>
    /// ایمیل
    /// </summary>
    public string? Email { get; set; } = string.Empty;
    /// <summary>
    /// Salt رمز عبور
    /// </summary>
    public string? Salt { get; set; }

    //public bool? Female { get; private set; }

    /// <summary>
    /// نام تصویر پروفایل
    /// </summary>
    public string? Picture { get; set; }


    /// <summary>
    /// کد تایید
    /// </summary>
    public string? VerifyCode { get; set; }



    /// <summary>
    /// تاریخ انقضای تایید
    /// </summary>
    public DateTime? VerifyExpire { get; private set; }





    #region ارتباطات


    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual ICollection<Slider> Sliders { get; set; }
    public virtual ICollection<CenterUserSelected> CenterUser { get; set; }
    public virtual ICollection<Blog> Blogs { get; set; }
    public virtual ICollection<BlogComment> BlogComments { get; set; }
    public virtual ICollection<CenterComment> CenterComments { get; set; } = [];
    #endregion

}



