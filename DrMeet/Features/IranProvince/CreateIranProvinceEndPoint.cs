using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.IranProvince;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.IranProvinces;


public static class CreateIranProvinceEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateIranProvince", handler: async (
                    IIranProvinceService service,
                      [FromBody] CreateIranProvinceRequestDto request
                ) =>
            {
     


                #region Import
                string json = File.ReadAllText(Directory.GetCurrentDirectory() + "/wwwroot/provinces.json");
                var Provinces_city = JsonConvert.DeserializeObject<List<ProvinceImport>>(json);


                foreach (var item in Provinces_city ?? new List<ProvinceImport>())
                {
                    await service.CreateIranProvinceAsync(new CreateIranProvinceRequestDto { Name = item.provinceName });
                }

                #endregion



                var result = await service.CreateIranProvinceAsync(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateIranProvinceRequestDto>());

        }
    }
}