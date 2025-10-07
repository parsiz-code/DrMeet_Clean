
using DrMeet.Domain.Base;
using DrMeet.Domain.Centers;

namespace DrMeet.Domain.Others;

public class Licenses : BaseEntityIdentity
{
    public string Name { get; set; }
    public int Order { get; set; }

    public ICollection<CenterLicensesSelected>? CenterLicensesSelected { get; set; }}
