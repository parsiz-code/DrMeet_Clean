using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Domian;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public class CreateDoctorRequestDto
{
    [Required(ErrorMessage ="نام را وارد کنید")]
    [MinLength(3,ErrorMessage =" نام باید بیشتر از 5 باشه")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "نام خوانوادگی   را وارد کنید")]
    [MinLength(3, ErrorMessage = " نام خوانوادگی باید بیشتر از 5 باشه")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "کد ملی را وارد کنید")]
        //[MinLength(9,ErrorMessage = " کد ملی باید بیشتر از 9 باشه")]
    public string NationalCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "شماره موبایل را واردکنید   را وارد کنید")]
    [MinLength(10, ErrorMessage = " شماره موبایل باید بیشتر از 10 باشه")]
    public string Mobile { get; set; } = string.Empty;
    [Required(ErrorMessage = "رمز را وارد کنید   را وارد کنید")]
    [MinLength(3, ErrorMessage = " رمز باید بیشتر از 5 باشه")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "تکراررمز را وارد   کنید   ")]
    [MinLength(3, ErrorMessage = " تکراررمز باید بیشتر از 5 باشه")]
    [Compare(nameof(Password),ErrorMessage ="رمز مطابقت ندارد")]
    public string ConfirmPassword { get; set; } = string.Empty;
    //public UserType UserType { get; set; }
   

}

public class CreateDoctorResponse
{
    public int Id { get; set; } 
}

public class CreateDoctorCenterDto
{
    public string Id { get; set; }
    public string Name { get; set; }

}