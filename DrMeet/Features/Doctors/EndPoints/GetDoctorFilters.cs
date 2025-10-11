using DrMeet.Api.Features.Iran.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Doctors.EndPoints;

public static class GetDoctorFilters
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/filters", handler: async (
                    IUnitOfWork uow
                ) =>
                {
                    var result = new GetDoctorFiltersResponse();

                    result.Days = PersianDay.PersianDayList;

                    result.Expertises = await uow.Expertise
                        .AsQueryable()
                        .Select(x => new ExpertiesDto(x.Id, x.Name))
                        .ToListAsync();

                    result.Province = (await uow.IranProvinces.GetAllAsync())
                        .Select(p => new IranProvinceDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Cities = p.Cities.Select(c => new IranCityDto
                            {
                                Id = c.Id,
                                Name = c.Name
                            }).ToList()
                        }).ToList();

                   // result.Services //todo
                    
                    
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag);
        }
    }

    public class GetDoctorFiltersResponse
    {
        public IReadOnlyCollection<PersianDay.PersianDayModel> Days { get; set; } = [];
        public List<ExpertiesDto> Expertises { get; set; } = [];
        
        public List<ExpertiesDto> Services { get; set; } = [];
        public List<IranProvinceDto> Province { get; set; } = [];
    }

    public record ExpertiesDto(int Id, string Name);
}