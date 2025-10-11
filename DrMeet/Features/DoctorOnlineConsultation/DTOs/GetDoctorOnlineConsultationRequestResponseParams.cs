using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;

public class GetDoctorOnlineConsultationRequestResponseParams : PagedParamData
{
    //public string? Title { get; set; }
    public string? DoctorId { get; set; }

}