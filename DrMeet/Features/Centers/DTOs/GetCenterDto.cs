namespace DrMeet.Api.Features.Centers.DTOs;

public class GetCenterDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string? MainPicture { get; set; } = string.Empty;
    public string? Address { get; set; }
}