namespace DrMeet.Api.Features.Iran.EndPoints.DTOs;

public record IranProvinceDto
{
    public int Id { get; init; } 
    public string Name { get; init; } = string.Empty;
    public List<IranCityDto> Cities { get; init; } = [];
}

public record IranCityDto
{
    public int Id { get; init; } 
    public string Name { get; init; } = string.Empty;
}