using DrMeet.Api.Features.Insurances.DTOs;

namespace DrMeet.Api.Features.Centers.DTOs;

public class CenterContractedSupplementalInsurancesResponseDto
{
    //بیمه های تکمیلی طرف قرارداد
    public List<GetInsuranceDetailResponseDto>? ContractedSupplementalInsurances { get; set; }
}


