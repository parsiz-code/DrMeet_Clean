using DrMeet.Api.Features.Blogs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Services.Blogs;
using DrMeet.Api.Shared.Services.Doctors;
namespace DrMeet.Api.Features.Doctors.EndPoints.Comment;


public static class GetCommentsDoctorIdEndPoint
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/GetCommentsDoctorId", handler: async (
                    IDoctorService doctorService,
                    [AsParameters] GetCommentsDoctorIdRequestResponseParams request

                ) =>
            {

                var data = await doctorService.GetCommentByDoctorAsync(request);
                if (data is null)
                    return BadRequest("داده ای یافت است");
                return Ok(data);
            })
                .WithTags(ApiInfo.Tag);

        }
    }
}
