using DrMeet.Api.Features.Doctors.EndPoints;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Services.Doctors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Doctors;

public static class UpdateDepartmanDoctorList
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateDepartmanDoctorList", handler: async (
                     IDoctorService doctorService,
                     [FromBody] UpdateDepartmanDoctorListRequestDto request
                ) =>
            {
             

                var result = await doctorService.AddDepartmanDoctorList(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })

            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<UpdateDepartmanDoctorListRequestDto>());

        }
    }
}