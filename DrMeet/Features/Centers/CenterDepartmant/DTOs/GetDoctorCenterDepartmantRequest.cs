using DrMeet.Api.Shared.PagedList;
using System.ComponentModel.DataAnnotations;

public class GetDoctorCenterDepartmantRequest: PagedParamData
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; } = 0;
    public string? Title { get; set; } = string.Empty;

    //[Required(ErrorMessage = "ساختمان  را انتخاب کنید")]
    //public int DepartmanId { get; set; }
    //[Required(ErrorMessage = " دکتر  را انتخاب کنید")]
    //public string DoctorId { get; set; } = string.Empty;

}
