using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.IranCitys.DTOs;

public class CreateIranCityRequestDto
{


    [Required(ErrorMessage = "شهر را انتخاب  کنید")]
    public string Name { get; set; }=string.Empty;
    public int ProvinceId { get; set; } 

}
