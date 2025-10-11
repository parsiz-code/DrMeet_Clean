using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.CenterTypes.DTOs;

public class CreateCenterTypeRequestDto
{
    [Required(ErrorMessage = "نوع مرکز را انتخاب  کنید")]
    public string Name { get; set; }

    public bool Status { get; set; }
}
