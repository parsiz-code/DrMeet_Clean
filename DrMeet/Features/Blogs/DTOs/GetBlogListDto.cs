namespace DrMeet.Api.Features.Blogs.DTOs;

    public class GetBlogListDto
    {

    public int Id { get; set; }
    public string Title { get; set; }
    public string? SummaryText { get; set; }
    public string Text { get; set; }
    public string ImagePath { get; set; }
    public AuthorDto Author { get; set; }
    public string Tags { get; set; }

    public DateTime CreateDate { get; set; }

}
