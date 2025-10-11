using DrMeet.Api.Features.Insurances.DTOs;

namespace DrMeet.Api.Features.Centers.DTOs;

public class CenterContractedBasicInsurancesResponseDto
{

    //بیمه های پایه طرف قرارداد
    public List<GetInsuranceDetailResponseDto>? ContractedBasicInsurances { get; set; }
}


