using DrMeet.Api.Shared.Domian;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorReserveTimes.DTOs;

public class CreateDoctorReserveTimeRequestDto
{
    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int DoctorShiftId { get; set; }
    [Required(ErrorMessage = "زمان را انتخاب کنید را انتخاب  کنید")]
    public int DoctorTimeId { get; set; }

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int PatientId { get; set; }

    [Required(ErrorMessage = "مرکز را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public required int CenterId { get; set; }
    public required DateTime Date { get; set; }



    public string? Description { get; set; }
}
