namespace DrMeet.Api.Features.Centers.Comment.DTOs;

public class UpdateStatusCommentCenterDto
{
    public int CenterId { get; set; }
    public int CommentId { get; set; }
    public bool Status { get; set; }
    
   
}
