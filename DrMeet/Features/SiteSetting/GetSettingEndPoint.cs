using DrMeet.Api.Features.Centers.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Setting;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.SiteSetting;

public static class GetSettingEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}", handler: async (
                    ISettingService settingService
                 
                ) =>
                {

                    var result = await settingService.GetSetting();
                    if (result == null)
                        return BadRequest("تنظیمات وجود ندارد");


                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}