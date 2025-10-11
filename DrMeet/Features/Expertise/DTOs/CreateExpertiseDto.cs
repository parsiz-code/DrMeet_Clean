using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Expertises.DTOs;

public class CreateExpertiseDto
{
    [Required(ErrorMessage = "نوع مرکز را انتخاب  کنید")]
    public string Name { get; set; }

}
