using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Insurances.DTOs;

public class GetInsuranceRequestResponseParams : PagedParamData
{
    public string? Title { get; set; }
    public bool IsBaseInsurance { get; set; }

}
