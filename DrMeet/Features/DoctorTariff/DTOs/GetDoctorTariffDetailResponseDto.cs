
using DrMeet.Api.Shared.Domian;

namespace DrMeet.Api.Features.DoctorTariffs.DTOs;

public class GetDoctorTariffDetailResponseDto
{
    public int Id { get; set; } 
    public float PercentagePayment { get; set; } 
    //   public Doctor Doctor { get; set; }
    public int DoctorId { get; set; } 
    public int CenterId { get; set; } 
    public int ServiceId { get; set; } 
    //  public ServicesAvailable ServicesAvailable { get; set; }
    public decimal Price { get; set; }

}
public class GetDoctorTariffDetailListResponseDto
{
    public string DoctorName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public int Id { get; set; }
    public decimal Price { get; set; }
 
}