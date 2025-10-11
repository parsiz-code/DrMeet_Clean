using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateGeneralCenterDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }

    [Required(ErrorMessage = "نام مرکز را انتخاب کنید")]
    public string Name { get; set; }
    public string FaxNumber { get; set; }

    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
    public string Email { get; set; }

    //[Url(ErrorMessage = "فرمت آدرس وب‌سایت معتبر نیست")]
    public string WebSite { get; set; }

    [StringLength(500, ErrorMessage = "آدرس نباید بیش از 500 کاراکتر باشد")]
    public string Address { get; set; }
    public List<string> Phone { get; set; }

    public string PhoneNumber { get; set; }

    [StringLength(1000, ErrorMessage = "توضیحات نباید بیش از 1000 کاراکتر باشد")]
    public string? Description { get; set; }

    [StringLength(1000, ErrorMessage = "بیوگرافی نباید بیش از 1000 کاراکتر باشد")]
    public string? Bio { get; set; }

    [DataType(DataType.Date, ErrorMessage = "فرمت تاریخ معتبر نیست")]
    public DateTime DateOfEstablishment { get; set; }
}
