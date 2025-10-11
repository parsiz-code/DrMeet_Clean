using System.Text.Json.Serialization;

using DrMeet.Api.Shared.Contracts;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using Mapster;

namespace DrMeet.Api.Features.Appointment;

public static class GetDoctorFreeTimes
{
    public class GetDoctorFreeTimesForReserveTimeResponse
    {
        public string Id { get; set; }

        [JsonIgnore] public DayOfWeek DayOfWeek { get; set; }
        public string DayName => PersianDay.PersianDayList[(int)DayOfWeek].Name;

        public DateTime Date { get; set; }

        //public int? CenterDoctorShiftRangeId { get; set; }

        public List<DoctorFreeTimeResponse> FreeTimes { get; set; } = [];
    }

    public class DoctorFreeTimeResponse
    {
        public string Id { get; set; } = string.Empty;

       // public int CenterDoctorShiftRangeId { get; set; }

        public TimeSpan Time { get; set; }

      //  public bool ReservedBefore { get; set; }
    }


    //public class EndPoint : BaseEndpoint, IEndpoint
    //{
    //    public void MapEndpoint(IEndpointRouteBuilder app)
    //    {
    //        app.MapGet($"{ApiInfo.Prefix}/available", handler: async (
    //                IUnitOfWork uow,
    //                int doctorId,
    //                int centerId,
    //                int departmentId
    //            ) =>

    //            {
    //                var doctor = await uow.Doctors.GetByIdAsync(doctorId);

    //                if (doctor is null)
    //                {
    //                    return BadRequest("اطلاعات پزشک یافت نشد");
    //                }

    //                var data = doctor
    //                    .CenterDoctors
    //                    .FirstOrDefault(x => x.CenterId == centerId &&
    //                                         x.CenterDoctorsDepartmant.Any(d => d.Id == departmentId));


                    
    //                //var result = data?.CenterDoctorsDepartmant
    //                //    .SelectMany(t => t.FreeReserveTimes)
    //                //    .Adapt<List<GetDoctorFreeTimesForReserveTimeResponse>>();


    //                return Ok(result ?? []);
    //            })
    //            .WithTags(ApiInfo.Tag);
    //    }
    //}
}