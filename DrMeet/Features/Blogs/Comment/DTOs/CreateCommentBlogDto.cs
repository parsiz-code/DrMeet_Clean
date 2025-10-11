namespace DrMeet.Api.Features.Blogs.Comment.DTOs;

public class CreateCommentBlogDto
{
    public int BlogId { get; set; }
    //public int CommentId { get; set; }

    public string Subject { get; set; }

    public string Text { get; set; }
    public int Score { get; set; }

}
