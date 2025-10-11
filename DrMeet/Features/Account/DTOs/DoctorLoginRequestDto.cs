using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Account.DTOs;

public record DoctorLoginRequestDto
{
    [Required(ErrorMessage = "وارد کردن نام  کاربری الزامی می باشد")]
    [MaxLength(100)]
    public string Username { get; init; } = string.Empty;

    [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی می باشد")]
    [MaxLength(100)]
    public string Password { get; init; } = string.Empty;
}
