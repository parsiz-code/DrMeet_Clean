using DrMeet.Api.Features.Doctors.EndPoints.DTOs;

using DrMeet.Api.Shared;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.Doctors;
using Mapster;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Doctors.EndPoints;

public static class GetDoctorDetail
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/{{id}}/Profile", handler: async (
                    IDoctorService doctorService,
                    int id
                ) =>
                {
                    var doctor = await doctorService.GetDoctorProfileByUserId(id);
                    
                    if (doctor is null)
                    {
                        return BadRequest("اطلاعات پزشک یافت نشد");
                    }

                    return Ok(doctor);
                })
                .WithTags(ApiInfo.Tag);
        }
    }
}