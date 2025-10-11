namespace DrMeet.Api.Features.Insurances.DTOs;

public class GetInsuranceDetailResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public bool Status { get; set; }
    public int Order { get; set; }
    public bool IsBaseInsurance { get; set; }

}
