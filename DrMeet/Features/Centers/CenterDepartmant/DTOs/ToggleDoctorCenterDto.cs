using System.ComponentModel.DataAnnotations;

public class ToggleDoctorCenterDto
{
    [Required(ErrorMessage = "مرکز را انتخاب کنید")]
    public int CenterId { get; set; } = 0;

    [Required(ErrorMessage = " دکتر  را انتخاب کنید")]
    public int DoctorId { get; set; } = 0;

}
