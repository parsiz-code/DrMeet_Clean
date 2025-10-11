using DrMeet.Api.Features.Account.DTOs;
 using DrMeet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Shared.Services.ParsizTeb.Models;

public record GlobalLoginDto
{

    [Required(ErrorMessage = "وارد کردن نام  کاربری الزامی می باشد")]
    [MaxLength(100)]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی می باشد")]
    [MaxLength(100)]
    public string Password { get; init; } = string.Empty;
    //public UserType UserType { get; set; }
    //public PatientTypeOrigin? PatientType { get; set; }
}
