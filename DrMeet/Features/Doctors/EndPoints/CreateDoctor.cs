using DrMeet.Api.Features.Blogs.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints;
using DrMeet.Api.Features.ServicesAvailables.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Doctors;
public static class CreateDoctor
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateDoctor", handler: async (
                     IDoctorService doctorService,
                     IJwtService jwtService,
                    [FromBody] CreateDoctorRequestDto request
                ) =>
            {
            

                var result =await doctorService.CreateDoctorAsync(request);
                if (result.IsError)
                    return BadRequest(string.Join(",", result.Errors.Select(_ => _.Description)));

                var token = jwtService.CreateToken(new LoginTokenRequest()
                {
                    userType=Account.DTOs.UserType.Doctor,
                    UserId=0,
                    Id=result.Value
                    //Id = result.Value
                });

                return Ok(new
                {
                    token
                });

               
            })
            .WithTags(ApiInfo.Tag).AddEndpointFilter(new ValidationFilter<CreateDoctorRequestDto>());

        }
    }
}