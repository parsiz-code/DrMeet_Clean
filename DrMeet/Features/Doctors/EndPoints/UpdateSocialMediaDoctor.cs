using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Features.Doctors.EndPoints;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Doctors;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using TeamLibrary.API.Shared.Tools.Helper;
namespace DrMeet.Api.Features.Doctors;
public static class UpdateSocialMediaDoctor
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}/UpdateSocialMediaDoctor", handler: async (
                     IDoctorService doctorService,
                     [FromBody] UpdateSocialMediaDoctorRequestDto request,
                      HttpContext httpContext
                ) =>
            {
                

                var DoctorId = httpContext.User.GetId(UserType.Doctor);
                var result = await doctorService.UpdateSocialMediaDoctor(request,DoctorId);
                if (result.ReturnResult == ReturnResult.Error)
                {
                    return BadRequest(result.LstMessage.GetString());
                }
                return Ok(result.LstMessage.GetString());
            })
       
            .WithTags(ApiInfo.Tag)
            .AddEndpointFilter(new ValidationFilter<UpdateSocialMediaDoctorRequestDto>())
              .RequireAuthorization()
           
            .AddEndpointFilter(async (context, next) =>
            {
                var user = context.HttpContext.User;
                var doctorId = user.GetAuthorizedUserId(UserType.Doctor);

                if (doctorId is null)
                    return BadRequest("دسترسی ندارید");


                return await next(context);
            });

        }
    }
}