using DNTPersianUtils.Core;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Domian.Doctors;
using DrMeet.Api.Shared.PagedList;
using Microsoft.OpenApi.Attributes;

namespace DrMeet.Api.Features.Doctors.EndPoints.DTOs;

public class GetDoctorListParams : PagedParamData
{
    public string? Name { get; set; }

    public WeekDay[]? WorkDays { get; set; }

    public string? ProvinceId { get; set; }

    public string? CityId { get; set; }
    public string? ExpertisesName { get; set; }

    public string? ServiceId { get; set; }
    public decimal? ServicePrice { get; set; }

    public GenderModel? Gender { get; set; }

    public double? Score { get; set; }
}

public enum OrderType
{
    [Display(" کمترین تعرفه")] MinPrice = 1,

    [Display(" بیشترین تعرفه")] MaxPrice = 2,

    [Display("نزدیک ترین نوبت")] NearestAppointment = 3,

    [Display("بالاترین امتیاز")] HighestScore = 4,

    [Display("بیشترین تجربه")] MostExperienced = 5
}