using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.ParsizTeb;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using DrMeet.Api.Shared.Services.UserService;
using Microsoft.Build.Framework;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Features.Appointment;

public static class CreateAppointment
{
    public record Request
    {
        [Required] public int DoctorId { get; init; } 

        [Required] public int CenterId { get; init; } 
        //int
        [Required] public int DepartmentId { get; init; }

        [Required] public int DateId { get; init; } 

        [Required] public int[] TimeIds { get; init; } = [];
    };


    public class EndPoint : BaseEndpoint, IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{ApiInfo.Prefix}", handler: async (
                    IParsizTebApiService parsizTebApi,
                    IUnitOfWork uow,
                    IUserService userService,
                    Request request
                ) =>
                {
                    var doctor = await uow.Doctors
                        .AsQueryable()
                        .FirstOrDefaultAsync(x => x.Id == request.DoctorId);

                    if (doctor is null)
                    {
                        return BadRequest("اطلاعات پزشک یافت نشد");
                    }

                    var doctorCenter = doctor.CenterDoctors
                        .FirstOrDefault(x => x.CenterId == request.CenterId);

                    if (doctorCenter is null)
                    {
                        return BadRequest("کد مرکز صحیح نمی باشد");
                    }

                    //var doctorDepartment = doctorCenter.ce
                    //    .FirstOrDefault(x => x.Id == request.DepartmentId);

                    //if (doctorDepartment is null)
                    //{
                    //    return BadRequest("کد بخش صحیح نمی باشد");
                    //}

                    //var date = doctorDepartment.FreeReserveTimes
                    //    .FirstOrDefault(x => x.Id == request.DateId);


                    //if (date is null)
                    //{
                    //    return BadRequest("کد تاریخ صحیح نمی باشد");
                    //}

                    //var requestedTimes = date
                    //    .FreeTimes
                    //    .Where(x => request.TimeIds.Contains(x.Id) && !x.ReservedBefore)
                    //    .ToList();


                    //if (requestedTimes.Count != request.TimeIds.Length)
                    //{
                    //    return BadRequest("کد زمان های انتخاب شده صحیح نمی باشد");
                    //}

                    //var patientId = userService.GetPatientId();

                    //await parsizTebApi.ReserveTimeAsync(new ReserveTimeDto()
                    //{
                    //    CenterId = request.CenterId,
                    //    CenterDoctorShiftRangeId = date.CenterDoctorShiftRangeId,
                    //    DepartmentId = request.DepartmentId,
                    //    PatientId = patientId,
                    //    DoctorId = doctor.RemoteDoctorId,
                    //    RequestDate = date.Date,
                    //    RequestTimes = requestedTimes.Select(x => x.Time).ToArray(),
                    //});

                    return Ok("نوبت شما با موفقیت ثبت گردید");
                })
                .WithTags(ApiInfo.Tag);
                //.RequireAuthorization();
        }
    }
}