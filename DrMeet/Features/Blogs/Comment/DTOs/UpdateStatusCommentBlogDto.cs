namespace DrMeet.Api.Features.Blogs.Comment.DTOs;

public class UpdateStatusCommentBlogDto
{
    public int BlogId { get; set; }
    public int CommentId { get; set; }
    public bool Status { get; set; }
    
   
}
