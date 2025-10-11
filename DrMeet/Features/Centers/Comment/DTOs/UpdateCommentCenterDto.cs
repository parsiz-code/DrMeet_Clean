namespace DrMeet.Api.Features.Centers.Comment.DTOs;

public class UpdateCommentCenterDto
{
    public int Id { get; set; }
    public int CenterId { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }
   
}
