using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Doctors.EndPoints.Comment;

public class GetCommentsDoctorIdRequestResponseParams : PagedParamData
{
    public required int DoctorId { get; set; }
}

