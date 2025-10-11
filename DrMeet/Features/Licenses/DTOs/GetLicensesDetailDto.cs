namespace DrMeet.Api.Features.Licenses.DTOs;

public class GetLicensesDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
    public int Order { get; set; }
}
