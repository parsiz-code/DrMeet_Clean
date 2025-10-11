namespace DrMeet.Api.Features.Insurances.DTOs;

    public class GetInsuranceListResponseDto
    {

    public int Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }

    public bool IsBaseInsurance { get; set; }

    public bool Status { get; set; }

}
