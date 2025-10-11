//using System.ComponentModel.DataAnnotations;

//namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

//public class UpdateProfileDoctorDto
//{

//    [Required(ErrorMessage = "شناسه پزشک الزامی است")]
//    public string? Id { get; set; } = string.Empty;
//    public string? FirstName { get; set; } = string.Empty;

//    public string? LastName { get; set; } = string.Empty;
//    public string? FatherName { get; set; } = string.Empty;
//    public string? Bio { get; set; } = string.Empty;
//    public string? ExpertiseId { get; set; } = null!;
//    public string? ExpertiseName { get; set; } = null!;
//    public string? Descrption { get; set; } = string.Empty;
//    public string? Province { get; set; } = string.Empty;
//    public string? City { get; set; } = string.Empty;
//    public string? Region { get; set; } = string.Empty;
//    public string? Over15YearsOfExperience { get; set; } = string.Empty;
//    public string? Password { get; set; } = string.Empty;
//    public string? BirthDate { get; set; }
//    public string? WebSite { get; set; } = string.Empty;
//    public string? NumberOfMedicalSystem { get; set; } = string.Empty;
//    public string? Email { get; set; } = string.Empty;
//    public IFormFile? Picture { get; set; }

//}
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class UpdateProfileDoctorRequestDto
{
    [Required(ErrorMessage = "شناسه پزشک الزامی است")]
    public int? Id { get; set; }

    [Required(ErrorMessage = "نام الزامی است")]
    [MaxLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "نام خانوادگی الزامی است")]
    [MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
    public string? LastName { get; set; }

    [MaxLength(50, ErrorMessage = "نام پدر نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
    public string? FatherName { get; set; }

    [MaxLength(1000, ErrorMessage = "بیوگرافی نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد")]
    public string? Bio { get; set; }

    [Required(ErrorMessage = "شناسه تخصص الزامی است")]
    public int? ExpertiseId { get; set; }

    //[Required(ErrorMessage = "نام تخصص الزامی است")]
    //public string? ExpertiseName { get; set; }

    [MaxLength(1000, ErrorMessage = "توضیحات نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد")]
    public string? Descrption { get; set; }

    [MaxLength(100, ErrorMessage = "استان نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public int? ProvinceId { get; set; }

    [MaxLength(100, ErrorMessage = "شهر نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public int? CityId { get; set; }

    [MaxLength(100, ErrorMessage = "منطقه نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
    public string? Region { get; set; }

    public bool? Over15YearsOfExperience { get; set; }

    [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
    public string? Password { get; set; }

    [DataType(DataType.Date, ErrorMessage = "فرمت تاریخ تولد نامعتبر است")]
    public string? BirthDate { get; set; }

    [Url(ErrorMessage = "آدرس وب‌سایت معتبر نیست")]
    public string? WebSite { get; set; }

    [MaxLength(10, ErrorMessage = "شماره نظام پزشکی نمی‌تواند بیشتر از ۱۰ کاراکتر باشد")]
    public string? NumberOfMedicalSystem { get; set; }

    [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
    public string? Email { get; set; }

    //public List<int>? CenterId { get; set; }

    public string? Picture { get; set; }

    
    public bool InPerson { get; set; }
    public bool IsVideoConsultation { get; set; }
    public bool IsPhoneConsultation { get; set; }
    // [ModelBinder(BinderType =(typeof(FromFormJsonBinder<CenterDepartmentDto>)))]
    //  public CenterDepartmentDto CenterDepartment { get; set; }
}

public class UpdateDepartmanDoctorListRequestDto
{
    public List<int> DoctorIds { get; set; }
    public UpdateCenterDepartmentRequestDto CenterDepartment { get; set; }
}
public class CenterDepartmentDto
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int CenterId { get; set; }

}
public class UpdateCenterDepartmentRequestDto
{
    public int DepartmentId { get; set; }
    public int CenterId { get; set; }

}

