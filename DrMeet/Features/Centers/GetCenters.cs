using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

using DrMeet.Api.Shared.Contracts;

using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Centers;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Centers;

public static class GetCenters
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (
                    IUnitOfWork uow,
                    [AsParameters] GetCenterParams request
                ) =>
                {
                    var query = uow.Centers.AsQueryable();
                    
                    if (request.Q.HasValue())
                    {
                        query = query.Where(x => x.Name.Contains(request.Q!));
                    }

                    var result = await query.ToPagedList<Center, GetCenterDto>(request.PageNumber, request.PageSize);
                    
                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}

public static class GetCenterDepartmants
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/Departmants", handler: async (
                    IUnitOfWork uow,
                    [AsParameters] ToggleDoctorCenterDto request
                ) =>
            {


                return Ok("");
            })
                .WithTags(ApiInfo.Tag);
        }
    }
}