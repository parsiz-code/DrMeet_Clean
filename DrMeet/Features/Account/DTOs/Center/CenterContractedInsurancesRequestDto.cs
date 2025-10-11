using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs;

public class CenterContractedInsurancesRequestDto: PagedParamData
{
    public string? Title { get; set; }
}
