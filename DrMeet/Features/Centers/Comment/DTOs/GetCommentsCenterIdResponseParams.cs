using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.Comment.DTOs;

public class GetCommentsCenterIdResponseParams : PagedParamData
{
    public required int CenterId { get; set; }
}

