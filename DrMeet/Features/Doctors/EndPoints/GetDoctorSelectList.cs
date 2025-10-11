using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian.Doctors;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Doctors.EndPoints;

public static class GetDoctorSelectList
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/SelectList", handler: async (
                    IUnitOfWork uow,
                    [AsParameters] GetDoctorSelectListParams request
                ) =>
                {
                    var query = uow.Doctors.AsQueryable();

                    if (request.CityId!=0)
                    {
                        var cityCenters = await uow.Centers
                            .AsQueryable()
                            .Where(x => x.CityId == request.CityId)
                            .Select(x => x.Id)
                            .ToListAsync();

                        //query = query.Where(x => x.CenterIds.Any(c => cityCenters.Contains(c)));
                    }

                    if (request.ExpertiseId!=0)
                    {
                        query = query.Where(x => x.DoctorExpertises.Select(_=>_.ExpertiseId).Equals(request.ExpertiseId));
                    }

                    if (request.Q.HasValue())
                    {
                        query = query.Where(x => x.User.FirstName.Contains(request.Q!) || x.User.LastName.Contains(request.Q!));
                    }

                    var result =
                        await query.ToPagedList<Doctor, GetDoctorSelectListDto>(request.PageNumber, 15);
                    
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}