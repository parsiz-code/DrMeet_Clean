using DrMeet.Api.Features.Expertises;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Expertise;

public static class GetExpertiseSelectList
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/SelectList", handler: async (
                    IUnitOfWork uow
                ) =>
                {
                    var expertises = await uow.Expertise
                        .AsQueryable()
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                        }).ToListAsync();

                    return Ok(expertises);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}