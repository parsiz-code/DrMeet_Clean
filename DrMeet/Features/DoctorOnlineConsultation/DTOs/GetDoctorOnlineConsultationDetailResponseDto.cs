using DrMeet.Api.Shared.Domian;

namespace DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;

public class GetDoctorOnlineConsultationDetailResponseDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public bool Status { get; set; }
}
