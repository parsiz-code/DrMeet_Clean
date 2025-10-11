using DrMeet.Api.Features.Account.DTOs;
using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Services.DoctorReserveTime;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;

namespace DrMeet.Api.Features.Patients;

public static class GetPatientAppointments
{
    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{ApiInfo.Prefix}/appointment", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IUserService userService,
                    IDoctorReserveTimeService doctorReserveTimeService,
                     HttpContext httpContext,
                    [AsParameters] GetPatientAppointmentsRequestResponseParamsDto request
                ) =>
                {
                    PagedList<GetPatientAppointmentsResponse> result=new PagedList<GetPatientAppointmentsResponse>();

                    var patientId = userService.GetPatientId();
                    if (patientId != 0)
                        result = await parsizTebApi.GetPatientAppointmentsAsync(request, patientId);
                    else
                    {
                        var localpatientId = httpContext.User.GetId(UserType.Patient);
                        result = await doctorReserveTimeService.GetPatientAppointmentsAsync(request, localpatientId);
                    }
                      

                    return Ok(result);
                })
                .WithTags(ApiInfo.Tag)
                .RequireAuthorization();
        }
    }
}