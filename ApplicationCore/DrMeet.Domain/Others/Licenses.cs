
using DrMeet.Domain.Base;

namespace DrMeet.Domain.Others;

public class Licenses : BaseEntityIdentity
{
    public string Name { get; set; }
    public int Order { get; set; }
}
