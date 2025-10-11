using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateCenterContractedBasicInsurancesDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }
    public List<int>? ContractedBasicInsurancesId { get; set; }
}
