using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Insurances.DTOs;

public class CreateInsuranceRequestDto
{
    [Required(ErrorMessage = "بیمه را انتخاب  کنید")]
    public string Name { get; set; }
    [Required(ErrorMessage = "عکس را انتخاب  کنید")]
    public string Picture { get; set; }
    [Required(ErrorMessage = "وضییت  را انتخاب  کنید")]
    public bool IsBaseInsurance { get; set; }
    public bool Status { get; set; }
}
