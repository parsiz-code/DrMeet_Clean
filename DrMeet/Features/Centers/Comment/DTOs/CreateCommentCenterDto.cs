namespace DrMeet.Api.Features.Centers.Comment.DTOs;

public class CreateCommentCenterDto
{
    public int CenterId { get; set; }
    //public int CommentId { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }

}
