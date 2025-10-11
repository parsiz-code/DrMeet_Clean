namespace DrMeet.Api.Features.IranProvinces.DTOs;

public class GetIranProvinceListResponseDto
{

    public int Id { get; set; }
    public string Name { get; set; }


}
public class ProvinceImport
{
    public string provinceName { get; set; }
    public string provinceId { get; set; }

}

public class CityImport
{
    public string cityName { get; set; }
    public string provinceName { get; set; }

}
