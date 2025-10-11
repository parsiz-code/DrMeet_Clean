
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.DoctorReserveTimes.DTOs;

public class UpdateDoctorReserveTimeRequestDto
{

    [Required(ErrorMessage = "شناسه را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int Id { get; set; }

    [Required(ErrorMessage = "شناسه شیفت  را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int DoctorTimeId { get; set; }

    [Required(ErrorMessage = "شناسه دکتر را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public int PatientId { get; set; }
    [Required(ErrorMessage = "توضیحات را واردکنید  کنید")]


    public string Description { get; set; }

    [Required(ErrorMessage = "مرکز را انتخاب  کنید")]
    [RegularExpression("^[a-fA-F0-9]{24}$", ErrorMessage = "شناسه باید ۲۴ رقم هگزادسیمال معتبر باشد")]
    public required int CenterId { get; set; }
    public ShiftStatus Status { get; set; } = ShiftStatus.Open;

}

