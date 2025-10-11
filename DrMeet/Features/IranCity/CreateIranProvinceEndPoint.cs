 using DrMeet.Api.Features.IranCitys.DTOs;
using DrMeet.Api.Features.IranProvinces;
using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.IranCity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.IranCitys;
public static class CreateIranCityEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateIranCity", handler: async (
                    IIranCityService service,
                    IUnitOfWork unitofwork,
                      [FromBody] CreateIranCityRequestDto request
                ) =>
            {
               

                #region Import
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/wwwroot/provinces_cities.json");
                var Provinces = JsonConvert.DeserializeObject<List<CityImport>>(json);


                foreach (var item in Provinces ?? new List<CityImport>())
                {
                    var province = await unitofwork.IranProvinces.AsQueryable().FirstOrDefaultAsync(_ => _.Name == item.provinceName);
                    await service.CreateIranCityAsync(new CreateIranCityRequestDto { Name = item.cityName,ProvinceId= province .Id});
                }

                #endregion

                var result = await service.CreateIranCityAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag)
               .AddEndpointFilter(new ValidationFilter<UpdateServicesAvailableRequestDto>())
            ;

        }
    }
}