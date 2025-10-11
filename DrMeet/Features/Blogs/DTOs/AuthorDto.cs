namespace DrMeet.Api.Features.Blogs.DTOs;

public class AuthorDto
{
    public AuthorDto()
    {
        CountOfBlog = 0;

    }
    public string Name { get; set; }
    public int CountOfBlog { get; set; } = 0;
    public string ImagePath { get; set; }
  

}