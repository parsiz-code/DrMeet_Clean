using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Insurances.DTOs;

public class GetInsuranceSelectListRequestResponseParams : PagedParamData
{
    public string? Q { get; set; }
    public bool IsBaseInsurance { get; set; }

}
