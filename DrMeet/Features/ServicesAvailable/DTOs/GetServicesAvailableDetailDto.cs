namespace DrMeet.Api.Features.ServicesAvailables.DTOs;

public class GetServicesAvailableDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public int Order { get; set; }
}
public class GetServicesAvailableDetailActiveResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
 
    public int Order { get; set; }
}