using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.DoctorTariffs.DTOs;

public class GetDoctorTariffRequestResponseParams : PagedParamData
{
    //public string? Title { get; set; }
    public int? DoctorId { get; set; }

}