using DrMeet.Api.Shared.Domian.Doctors;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Doctors.EndPoints.Remote.DTOs;

public class CreateRemoteDoctorRequestDto
{
    [Required(ErrorMessage = "دکتر  را وارد کنید")]
    public int RemoteDoctorId { get; set; }
    public int DayVisitTime { get; set; }
    [Required(ErrorMessage = "نام را وارد کنید")]
    [MinLength(3, ErrorMessage = " نام باید بیشتر از 5 باشه")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "نام خوانوادگی   را وارد کنید")]
    [MinLength(3, ErrorMessage = " نام خوانوادگی باید بیشتر از 5 باشه")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "کد ملی را وارد کنید")]
    [MinLength(9, ErrorMessage = " کد ملی باید بیشتر از 9 باشه")]
    public string NationalCode { get; set; } = string.Empty;
    [Required(ErrorMessage = "شماره موبایل را واردکنید   را وارد کنید")]
    [MinLength(10, ErrorMessage = " شماره موبایل باید بیشتر از 10 باشه")]
    public string Mobile { get; set; } = string.Empty;
    public string EmergencyPhone { get; set; } = string.Empty;
    public string ExpertiseName { get; set; } = string.Empty;
    public string BirthDay { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    [Required(ErrorMessage = "جنیست را واردکنید   را وارد کنید")]
    public GenderModel Gender { get; set; }
    [Required(ErrorMessage = "شماره نظام پزشکی را واردکنید   را وارد کنید")]
    public string? NumberMedicalSystem { get; set; } = string.Empty;
    public string? Picture { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Website { get; set; } = string.Empty;
    public string? Bio { get; set; } = string.Empty;
    public string DoctorGroup { get; set; } = string.Empty;
    public bool OverFifteenYearsExperience { get; set; } = false;
    [Required(ErrorMessage = "شهر  را وارد کنید")]
    public string City { get; set; } 
    [Required(ErrorMessage = "استان  را وارد کنید")]
    public string Province { get; set; } 


    //public UserType UserType { get; set; }


}
