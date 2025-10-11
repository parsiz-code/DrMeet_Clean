using DrMeet.Api.Features.Doctors.EndPoints;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Doctors;
public static class UpdateDepartmanDoctor
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateDepartmanDoctor", handler: async (
                     IDoctorService doctorService,
                     [FromBody] UpdateDepartmanDoctorRequestDto request
                ) =>
            {
               


                var result = await doctorService.AddDepartmanDoctor(request);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
       
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<UpdateDepartmanDoctorRequestDto>());

        }
    }
}
