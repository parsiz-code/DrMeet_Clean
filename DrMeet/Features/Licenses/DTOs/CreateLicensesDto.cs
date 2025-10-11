using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Licenses.DTOs;

public class CreateLicensesDto
{
    [Required(ErrorMessage = "نام مجوز را انتخاب  کنید")]
    public string Name { get; set; }

    public bool Status { get; set; }
}
