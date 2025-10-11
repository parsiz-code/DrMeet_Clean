using System.ComponentModel.DataAnnotations;

public class AddDoctorCenterDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; } = 0;

    [Required(ErrorMessage = "ساختمان  را انتخاب کنید")]
    public int DepartmanId { get; set; }
    [Required(ErrorMessage = " دکتر  را انتخاب کنید")]
    public int DoctorId { get; set; } = 0;

}
