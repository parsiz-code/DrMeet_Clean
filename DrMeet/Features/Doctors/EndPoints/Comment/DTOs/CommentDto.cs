namespace DrMeet.Api.Features.Doctors.Comment.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? Email { get; set; }
    public string? Text { get; set; }
    public int Score { get; set; }
    public bool Status { get; set; } = false;

}

