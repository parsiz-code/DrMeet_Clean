namespace DrMeet.Api.Features.Doctors.Comment.DTOs;

public class DeleteDoctorCommentRequestDto
{
    public int CommentId { get; set; }
    public int DoctorId { get; set; }
}
