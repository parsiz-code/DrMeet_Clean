using DrMeet.Api.Shared.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Centers.DTOs;

    public class UpdateCenterDto
{
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public string Id { get; set; }
    [Required(ErrorMessage = "نام مرکز الزامی است")]
    [StringLength(100, ErrorMessage = "نام مرکز نباید بیش از 100 کاراکتر باشد")]
    public string Name { get; set; } = string.Empty;
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه   نوع مرکز باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    //[Required(ErrorMessage = "نوع مرکز الزامی است")]
    //public string CenterTypeId { get; set; } = string.Empty;

    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه   مرکز باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    //public string? CenterId { get; set; } = string.Empty;

    [Required(ErrorMessage = "شناسه شهر الزامی است")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه شهر باید ۲۴ رقم هگزادسیمال معتبر باشد")]

    public string CityId { get; set; }

    [Required(ErrorMessage = "شناسه استان الزامی است")]
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه استان باید ۲۴ رقم هگزادسیمال معتبر باشد")]

    public string ProvinceId { get; set; }

    [StringLength(100, ErrorMessage = "نام منطقه نباید بیش از 100 کاراکتر باشد")]
    public string Region { get; set; }

    [StringLength(1000, ErrorMessage = "بیوگرافی نباید بیش از 1000 کاراکتر باشد")]
    public string Bio { get; set; }

    [DataType(DataType.Date, ErrorMessage = "فرمت تاریخ معتبر نیست")]
    public DateTime DateOfEstablishment { get; set; }

    [StringLength(100, ErrorMessage = "نام مدیر نباید بیش از 100 کاراکتر باشد")]
    public string AdminName { get; set; }

    //[Phone(ErrorMessage = "شماره همراه معتبر نیست")]
    public string PhoneNumber { get; set; }

    public List<string> Phone { get; set; }

    public string FaxNumber { get; set; }

    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
    public string Email { get; set; }

    [Url(ErrorMessage = "فرمت آدرس وب‌سایت معتبر نیست")]
    public string WebSite { get; set; }

    [StringLength(500, ErrorMessage = "آدرس نباید بیش از 500 کاراکتر باشد")]
    public string Address { get; set; }
    //[RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه استان باید ۲۴ رقم هگزادسیمال معتبر باشد")]

    public List<string>? Licenses { get; set; }

    [StringLength(1000, ErrorMessage = "توضیحات نباید بیش از 1000 کاراکتر باشد")]
    public string Description { get; set; }
    //[MaxFileSize(200)] // محدود به ۲۰۰ کیلوبایت
    public string Picture { get; set; }
    public string PictureType { get; set; }

    public List<string>? ServicesAvailable { get; set; }

    public List<string>? ContractedBasicInsurances { get; set; }

    public List<string>? ContractedSupplementalInsurances { get; set; }


}

