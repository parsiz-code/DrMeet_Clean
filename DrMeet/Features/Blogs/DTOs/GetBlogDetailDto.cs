namespace DrMeet.Api.Features.Blogs.DTOs;

public class GetBlogDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? SummaryText { get; set; }
    public string Text { get; set; }
    public string ImagePath { get; set; }
    public string ReadTime { get; set; }
    public AuthorDto Author { get; set; }
    public string Tags { get; set; }
    public List<BlogCommentDto> Comment { get; set; }

    public DateTime CreateDate { get; set; }
}
