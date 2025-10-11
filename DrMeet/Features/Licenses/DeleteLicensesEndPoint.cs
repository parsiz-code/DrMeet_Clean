using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Licensess;

namespace DrMeet.Api.Features.Licenses;
public class DeleteLicensesEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete($"{ApiInfo.Prefix}/DeleteLicenses/{{LicensesId}}", handler: async (
                    ILicensesService service,
                    int LicensesId
                ) =>
            {
                var result = await service.DeleteLicenses(LicensesId);

                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
                .WithTags(ApiInfo.Tag);
            //.RequireAuthorization();
        }
    }
}