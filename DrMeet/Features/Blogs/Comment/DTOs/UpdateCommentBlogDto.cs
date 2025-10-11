namespace DrMeet.Api.Features.Blogs.Comment.DTOs;

public class UpdateCommentBlogDto
{
    public int Id { get; set; }
    public int BlogId { get; set; }

    public string Subject { get; set; }
    public string Text { get; set; }
    public int Score { get; set; }
   
}
