using DrMeet.Api.Shared.PagedList;

namespace DrMeet.Api.Features.Centers.DTOs
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class NearbyLocationDto: PagedParamData
    {
        public int CenterId { get; set; }
        public double RadiusInMeters { get; set; } = 5000;
    }
}
