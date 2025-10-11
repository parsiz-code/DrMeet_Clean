using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.ServicesAvailables.DTOs;

public class CreateServicesAvailableRequestDto
{
    [Required(ErrorMessage = "نوع خدمات را انتخاب  کنید")]
    public string Name { get; set; }

    public bool Status { get; set; }
}
