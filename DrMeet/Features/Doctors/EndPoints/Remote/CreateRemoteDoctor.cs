using DrMeet.Api.Features.Doctors.EndPoints.Remote.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.JwtService;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DrMeet.Api.Features.Doctors.EndPoints.Remote;
public static class CreateRemoteDoctor
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/CreateDoctor", handler: async (
                     IDoctorService doctorService,
                     IJwtService jwtService,
                    [FromBody] CreateRemoteDoctorRequestDto request
                ) =>
            {
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(request);
                bool isValid = Validator.TryValidateObject(request, context, validationResults, true);

                if (!isValid)
                {
                    var errors = validationResults.Select(v => v.ErrorMessage).ToList();
                    return BadRequest(string.Join(",",errors));
                }



                var result =await doctorService.CreateDoctorAsync(request);
                if (result.IsError)
                    return BadRequest(string.Join(",", result.Errors.Select(_ => _.Description)));

              
                return Ok("دکتر با موفقیت ثبت شد");

               
            })
            .WithTags(ApiInfo.Tag);

        }
    }
}