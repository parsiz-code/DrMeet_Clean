using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Domian;

namespace DrMeet.Api.Features.Centers.DTOs;

    public class GetCenterDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int? CenterId { get; set; } 
    public int? CityId { get; set; }
    public string? CityName { get; set; }

    public string? ProvinceName { get; set; }
    public int? ProvinceId { get; set; }
    public string? Region { get; set; }
    public string? Bio { get; set; }
    public string? DateOfEstablishment { get; set; }
    public string? AdminName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Phone { get; set; }
    //شماره نمابر
    public string? FaxNumber { get; set; }
    public string? Email { get; set; }
    public string? WebSite { get; set; }
    public string? Address { get; set; }
    public List<string>? Licenses { get; set; }
    public string? Description { get; set; }
    public LocationDto? Location { get; set; }
    public string? MainPicture { get; set; }
    public List<int> CenterIds { get; set; } = [];

}

public class GetCenterDetailResponseDto
{
    public GetCenterDetailDto Center { get; set; }
    public  List<CenterItemServicesAvailable>? ServicesAvailable { get; set; }
    public  List<GetInsuranceDetailResponseDto>? ContractedSupplementalInsurances { get; set; }
    public  List<GetInsuranceDetailResponseDto>? ContractedBasicInsurances { get; set; }
    public List<CenterDepartmentDto>? CenterDepartment { get; set; }
    public List<PictureCenterDto>? Picture { get; set; }
}


