using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Expertises.DTOs;

public class GetExpertiseResponseParams : PagedParamData
{
    public string? Title { get; set; }

}
