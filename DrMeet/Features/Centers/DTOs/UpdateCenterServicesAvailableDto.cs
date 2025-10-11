using System.ComponentModel.DataAnnotations;
namespace DrMeet.Api.Features.Centers.DTOs;

public class UpdateCenterServicesAvailableDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; }
    public List<UpdateServicesDoctorDto>? ServicesDoctor { get; set; }
}
public class UpdateServicesDoctorDto
{
    
    [Required(ErrorMessage = "خدمت  را انتخاب کنید")]
    public int ServicesAvailableId { get; set; }
   
}