namespace DrMeet.Api.Features.Sliders.DTOs;

public class GetSliderDetailResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImagePath { get; set; }

    public DateTime CreateDate { get; set; }
}
