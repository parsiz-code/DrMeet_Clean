using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Domian.Doctors;

namespace DrMeet.Api.Features.DoctorTariffs.DTOs;

    public class GetDoctorTariffListDto
    {
    public string Id { get; set; }
    public Doctor Doctor { get; set; }

    public decimal Price { get; set; }
    public bool Status { get; set; }


}
