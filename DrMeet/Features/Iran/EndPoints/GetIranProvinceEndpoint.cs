using DrMeet.Api.Features.Iran.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Persistence.UnitOfWork;

namespace DrMeet.Api.Features.Iran.EndPoints;

public static class GetIranProvinceEndpoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/province", handler: async (
                    IUnitOfWork uow
                ) =>
                {
                    var data = (await uow.IranProvinces.GetAllAsync())
                        .Select(p => new IranProvinceDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Cities = uow.IranCities.AsQueryable().Where(_ => _.ProvinceId == p.Id).Select(_ =>new IranCityDto
                            {
                                Id=_.Id,
                                Name=_.Name,
                            }).ToList()
                        }).ToList();
                    ;
                    return Ok(data);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}