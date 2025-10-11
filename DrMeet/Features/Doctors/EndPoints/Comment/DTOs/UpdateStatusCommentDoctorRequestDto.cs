namespace DrMeet.Api.Features.Doctors.Comment.DTOs;

public class UpdateStatusCommentDoctorRequestDto
{
    public int DoctorId { get; set; }
    public int CommentId { get; set; }
    public bool Status { get; set; }
    
   
}
