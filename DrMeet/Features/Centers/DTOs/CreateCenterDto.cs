using DrMeet.Api.Features.Centers.DTOs.CenterDepartmen;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared.CustomAttribute;
using DrMeet.Api.Shared.Domian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;
public class CreateCenterDto
{
    [Required(ErrorMessage = "نام مرکز الزامی است")]
    [StringLength(100, ErrorMessage = "نام مرکز نباید بیش از 100 کاراکتر باشد")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "نوع مرکز الزامی است")]
    public int CenterTypeId { get; set; } 

    [Required(ErrorMessage = "شناسه شهر الزامی است")]


    public int CityId { get; set; }

    [Required(ErrorMessage = "شناسه استان الزامی است")]
  

    public int ProvinceId { get; set; }

    [StringLength(100, ErrorMessage = "نام منطقه نباید بیش از 100 کاراکتر باشد")]
    public string? Region { get; set; }
 

    [StringLength(100, ErrorMessage = "نام مدیر نباید بیش از 100 کاراکتر باشد")]
    public string AdminName { get; set; }

    [StringLength(100, ErrorMessage = "نام مدیر نباید بیش از 100 کاراکتر باشد")]
    public string Password { get; set; }

}
