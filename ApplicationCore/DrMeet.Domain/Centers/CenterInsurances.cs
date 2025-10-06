using DrMeet.Domain.Base;
using DrMeet.Domain.Others;

namespace DrMeet.Domain.Centers;

public class CenterInsurances : BaseEntityIdentity
{

    public Insurance? Insurance { get; set; }
    public int? InsuranceId { get; set; }

    public int? CenterId { get; set; }


    public Center? Center { get; set; }


}
