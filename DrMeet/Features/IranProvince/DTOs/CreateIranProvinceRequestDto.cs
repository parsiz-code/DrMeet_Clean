using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.IranProvinces.DTOs;

public class CreateIranProvinceRequestDto
{
    [Required(ErrorMessage = "نوع مرکز را انتخاب  کنید")]
    public string Name { get; set; }

}
