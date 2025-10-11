using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs
{
    public class GetCenterResponseParams : PagedParamData
    {
        public string? Title { get; set; }
    }
    public class GetDoctorCenterResponseParams : PagedParamData
    {
        public string? Title { get; set; }
        public int? CenterId { get; set; }
    }
}
