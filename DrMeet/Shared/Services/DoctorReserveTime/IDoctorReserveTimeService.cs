using DNTCommon.Web.Core;

using DrMeet.Api.Features.DoctorReserveTimes.DTOs;

using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Api.Shared.Services.ParsizTeb.Models;
using ErrorOr;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.DoctorReserveTime;

public interface IDoctorReserveTimeService : IDisposable
{
    #region DoctorReserveTime

    Task<ReturnUiResult> CreateDoctorReserveTimeAsync(CreateDoctorReserveTimeRequestDto dto);
    Task<ReturnUiResult> EditDoctorReserveTime(UpdateDoctorReserveTimeRequestDto dto);
    Task<ReturnUiResult> DeleteDoctorReserveTime(int DoctorReserveTimeId);
    Task<GetDoctorReserveTimeDetailResponseDto> GetDoctorReserveTime(int DoctorReserveTimeId);

    Task<PagedList<GetDoctorReserveTimeListResponseDto>> GetDoctorReserveTimes(GetDoctorReserveTimeRequestResponseParams request);
    Task<ErrorOr<Dictionary<int, GetFreeTimeDoctorReserveTimeListResponseDto>>> GetDoctorReserveTimes(GetDoctorFreeTimeReserveTimeRequestDto request);
    Task<ErrorOr<GetFreeOneTimeDoctorReserveTimeListResponseDto>> GetDoctorReserveTimes(GetDoctorFreeOneTimeReserveTimeRequestDto request);
    Task<PagedList<GetPatientAppointmentsResponse>> GetPatientAppointmentsAsync(GetPatientAppointmentsRequestResponseParamsDto request, int patientId);

    #endregion
}
public class DoctorReserveTimeService : IDoctorReserveTimeService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public DoctorReserveTimeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region DoctorReserveTime

    public async Task<ReturnUiResult> CreateDoctorReserveTimeAsync(CreateDoctorReserveTimeRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctorShift = await _unitOfWork.DoctorShifts.GetByIdAsync(dto.DoctorShiftId);
            var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);


            if (doctorShift == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شیفت یافت نشد");
                return returnUiResult;

            }
            var doctorTime = doctorShift.DoctorShiftTimeItems.Where(a => a.Id.Equals(dto.DoctorTimeId)).FirstOrDefault();
            if (doctorTime == null)
            {

                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("زمان یافت نشد");
                return returnUiResult;

            }
            if (!doctorTime.IsShiftAvailable)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نوبت در این زمان امکان پذیر نمیباشد");
                return returnUiResult;
            }
            if (patient == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("بیمار یافت نشد");
                return returnUiResult;

            }

            var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;

            }
            var centerDoctorsService = await _unitOfWork.CenterDoctorsServiceSelected
                .AsQueryable()
                .Where(_ =>

                _.CenterId == dto.CenterId &&
                _.DoctorId == doctorShift.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId &&
                doctorTime.DoctorShift.DoctorShiftServices.Select(s=>s.CenterDoctorsServiceSelected.ProviderServiceId).Equals(_.ProviderServiceId)

                )
                .FirstOrDefaultAsync();
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;

            }

            DrMeet.Domain.Doctors.DoctorReserveTime DoctorReserveTime = new DrMeet.Domain.Doctors.DoctorReserveTime()
            {
                CenterDoctorsServiceId = 1,
                
                Patient = patient,
                PatientId = dto.PatientId,
            
                Description = dto?.Description ?? string.Empty,
                ShiftStatus = ShiftStatus.Open,
             

                DoctorTimeId = dto.DoctorTimeId,
                Date = dto.Date,
            };
            //doctorTime.IsShiftAvailable = false;

            await _unitOfWork.DoctorShifts.UpdateAsync(doctorShift);
            await _unitOfWork.DoctorReserveTimes.AddAsync(DoctorReserveTime);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت رزرو نوبت با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت رزرو نوبت با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditDoctorReserveTime(UpdateDoctorReserveTimeRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;

            }

            var DoctorReserveTime = await _unitOfWork.DoctorReserveTimes.GetByIdAsync(dto.Id);

            if (DoctorReserveTime == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("رزرو یافت نشد");
                return returnUiResult;
            }
            var doctorShift = await _unitOfWork.DoctorShiftTimeItem.GetByIdAsync(dto.DoctorTimeId);
            if (doctorShift == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شیفت  یافت نشد");
                return returnUiResult;

            }
            var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
            if (patient == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("بیمار یافت نشد");
                return returnUiResult;

            }

            DoctorReserveTime.PatientId = dto.PatientId;

            DoctorReserveTime.DoctorTimeId = dto.DoctorTimeId;
            DoctorReserveTime.ShiftStatus = dto.Status;
            DoctorReserveTime.Description = dto.Description;



            await _unitOfWork.DoctorReserveTimes.UpdateAsync(DoctorReserveTime);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت رزرو نوبت با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت رزرو نوبت  با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteDoctorReserveTime(int DoctorReserveTimeId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var DoctorReserveTime = await _unitOfWork
                .DoctorReserveTimes
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == DoctorReserveTimeId);

            if (DoctorReserveTime == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("رزرو نوبت  یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.DoctorReserveTimes.DeleteAsync(DoctorReserveTimeId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف رزرو نوبت  با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetDoctorReserveTimeDetailResponseDto> GetDoctorReserveTime(int DoctorReserveTimeId)
    {
        try
        {
            var DoctorReserveTime = await _unitOfWork
                .DoctorReserveTimes
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == DoctorReserveTimeId)
                .Select(s => new GetDoctorReserveTimeDetailResponseDto
                {
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (DoctorReserveTime == null)
                return null;

            return DoctorReserveTime;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetDoctorReserveTimeListResponseDto>> GetDoctorReserveTimes(GetDoctorReserveTimeRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetDoctorReserveTimeListResponseDto>();

            var DoctorReserveTimeDtos = _unitOfWork
                 .DoctorReserveTimes
                 .AsQueryable()
                 //.Where(_=>_.ShiftStatus==ShiftStatus.)
                 ;


            var result = await DoctorReserveTimeDtos.ToPagedList(s => new GetDoctorReserveTimeListResponseDto
            {
                DoctorShiftId = s.DoctorShiftTimeItem.DoctorShiftId,
                PatientId = s.PatientId,
                Id = s.Id
            }, request.PageNumber, request.PageSize);

            return result;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }


    public async Task<ErrorOr<Dictionary<int, GetFreeTimeDoctorReserveTimeListResponseDto>>> GetDoctorReserveTimes(GetDoctorFreeTimeReserveTimeRequestDto request)
    {
        try
        {
            var response = new PagedList<GetDoctorReserveTimeListResponseDto>();

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);
            List<int?> timeIds = new List<int?>();
            var doctorShifts = _unitOfWork.DoctorShifts.AsQueryable().Where(_ => _.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Equals(request.DoctorId));
            if (request.DayOfWeek.HasValue)
            {
                var curentDateByWeekDay = request.DayOfWeek.Value.GetDateByDayOfWeek();
                doctorShifts = doctorShifts.Where(_ => _.DayOfWeek == request.DayOfWeek.Value);
                timeIds = await _unitOfWork.DoctorReserveTimes.AsQueryable()
                   .Where(_ => _.CenterDoctorsServiceSelected.DoctorId.Equals(request.DoctorId) &&

                   curentDateByWeekDay.Year == _.Date.Year &&
                   curentDateByWeekDay.Month == _.Date.Month &&
                   curentDateByWeekDay.Day == _.Date.Day

                   )
                   .Select(_ => _.DoctorTimeId)
                   .ToListAsync();

                var s = doctorShifts.ToList();
            }

            if (request.Date.HasValue)
            {

                var a = PersianDay.GetTodayWeekDay(request.Date.Value);

                doctorShifts = doctorShifts.Where(_ => _.DayOfWeek == a);
                timeIds = await _unitOfWork.DoctorReserveTimes.AsQueryable()
                    .Where(_ => _.CenterDoctorsServiceSelected.DoctorId.Equals(request.DoctorId) && request.Date.Value.Date == _.Date.Date)
                    .Select(_ => _.DoctorTimeId)
                    .ToListAsync();

                var s = doctorShifts.ToList();
            }


            if (doctor == null)
                return Error.NotFound("دکتر", "دکتر یافت نشد");
            if (request.DayOfWeek.HasValue)
            {
                if (doctorShifts.Count() == 0)
                    return Error.NotFound("شیفت دکتر", $"در روز{PersianDay.GetNameOfDay(request.DayOfWeek.Value)} پزشک شیفت ندارد");

            }




            var groupedShifts = doctorShifts
         .OrderBy(ds => ds.ShiftType)
         .GroupBy(ds => ds.ShiftType)
         .ToDictionary(
             g => (int)g.Key,
             g => new GetFreeTimeDoctorReserveTimeListResponseDto
             {
                 Date = request.Date.HasValue ? request.Date.Value : request.DayOfWeek.Value.GetDateByDayOfWeek(),
                 ShiftId = g.Where(_ => _.ShiftType == g.Key).Select(_ => _.Id).FirstOrDefault(),
                 TurnTimes = g
                     .SelectMany(ds => ds.DoctorShiftTimeItems.Select(st => new
                     {
                         ShiftId = ds.Id,
                         ShiftTime = st,
                         ActivityStatus = ds.ActivityStatus
                     }))
                     //.Where(x => x.ShiftTime.IsShiftAvailable)
                     .Select(x => new ShiftTimeItemDto
                     {
                         TimeId = x.ShiftTime.Id,
                         StartTime = x.ShiftTime.StartTime,
                         EndTime = x.ShiftTime.EndTime,
                         IsShiftAvailable = x.ShiftTime.IsShiftAvailable,

                     }).Where(_ => !timeIds.Contains(_.TimeId)).ToList()
             });



            return groupedShifts;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return Error.Conflict("خطای نامشخص رخ داده");
        }
    }
    public async Task<ErrorOr<GetFreeOneTimeDoctorReserveTimeListResponseDto>> GetDoctorReserveTimes(GetDoctorFreeOneTimeReserveTimeRequestDto request)
    {
        try
        {
            DateTime rezerveTime = request.Date.HasValue ? request.Date.Value : DateTime.Now;

            //var targetDate = request.Date.Value.Date;
            //var targetDayOfWeek = (WeekDay)targetDate.DayOfWeek;

            List<int?> timeIds = new List<int?>();

            GetFreeOneTimeDoctorReserveTimeListResponseDto response = new GetFreeOneTimeDoctorReserveTimeListResponseDto();

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId);
            var doctorShifts = await _unitOfWork.DoctorShifts.AsQueryable()
                .Where(s => s.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId == request.DoctorId && s.CenterDoctorsDepartmant.CenterDepartment.CenterId == request.CenterId &&
                s.ActivityStatus == ShiftActivityStatus.Active
                //s.DayOfWeek == targetDayOfWeek

                )
                .OrderBy(a => (int)a.DayOfWeek)
                .ThenBy(a => (int)a.ShiftType).ToListAsync();

            if (doctorShifts == null || doctorShifts.Count == 0)
                return Error.NotFound("دکتر", "دکتر در این روز شیفتی ندارد");

            if (doctor == null)
                return Error.NotFound("دکتر", "دکتر یافت نشد");



            var doctorShift = doctorShifts.ToList();

            bool flag = true;

            while (flag && doctorShift.Count > 0)
            {
                var curentDay = PersianDay.GetTodayWeekDay(rezerveTime);
                var doctorShifts1 = doctorShifts.Where(_ => _.DayOfWeek == curentDay).ToList() ?? new();
                foreach (var item in doctorShifts1)
                {
                    timeIds = await _unitOfWork.DoctorReserveTimes.AsQueryable()
                    .Where(_ => _.CenterDoctorsServiceSelected.DoctorId.Equals(request.DoctorId) && rezerveTime.Date == _.Date.Date)
                    .Select(_ => _.DoctorTimeId)
                    .ToListAsync();
                    response = doctorShifts1
                .SelectMany(shift => shift.DoctorShiftTimeItems
                    .Where(time => time.IsShiftAvailable && !timeIds.Contains(time.Id))
                    .Select(time => new GetFreeOneTimeDoctorReserveTimeListResponseDto
                    {
                        ShiftId = shift.Id,
                        TimeId = time.Id,
                        StartTime = time.StartTime,
                        DayOfWeek = shift.DayOfWeek,
                        IsToday = (rezerveTime.Year == DateTime.Now.Year &&
                                    rezerveTime.Month == DateTime.Now.Month &&
                                    rezerveTime.Day == DateTime.Now.Day)
                                   ,
                        Date = rezerveTime,
                    }))
                .Where(_ => !timeIds.Contains(_.TimeId))
                .FirstOrDefault();
                    if (response is not null)
                        flag = false;
                }
                rezerveTime = rezerveTime.AddDays(1);

            }


            return response;

        }
        catch (Exception)
        {
            // ثبت لاگ
            return Error.Conflict("error", "خطای نامشخص رخ داده");
        }
    }


    public async Task<PagedList<GetPatientAppointmentsResponse>> GetPatientAppointmentsAsync(GetPatientAppointmentsRequestResponseParamsDto request, int patientId)
    {
        try
        {
            var result = new PagedList<GetPatientAppointmentsResponse>();

            var isExistsPatient = await _unitOfWork.Patients.AsQueryable().AnyAsync(_ => _.Id == patientId);
            if (!isExistsPatient)
                return null;

            var reserveTime = _unitOfWork.DoctorReserveTimes
                .AsQueryable()
                .Where(_ => _.PatientId == patientId);

            if (request.CenterId is not null)
                reserveTime.Where(_ => _.CenterDoctorsServiceSelected.CenterId.HasValue && _.CenterDoctorsServiceSelected.CenterId.Value == request.CenterId);
            foreach (var s in reserveTime)
            {
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(s.CenterDoctorsServiceSelected.DoctorId.Value);

                result.List.Add(new GetPatientAppointmentsResponse
                {
                    CenterId = s.CenterDoctorsServiceSelected.CenterId.Value,
                    CenterName = s.CenterDoctorsServiceSelected.Center.Name,
                    Date = DateOnly.FromDateTime(s.Date),
                    Time = TimeSpan.Parse(s.DoctorShiftTimeItem.StartTime),
                    IsVisited = s.ShiftStatus == ShiftStatus.Completed,
                    DoctorFullName = doctor?.User.FirstName ?? "نام ندارد"
                });
            }
            int totalPage = result.List.Count / request.PageSize.Value;
            result.Pagination = new PagedListInfo
            {
                PageNumber = request.PageNumber.Value,
                PageSize = request.PageSize.Value,
                TotalCount = result.List.Count,
                TotalPages = totalPage
            };
            return result;
        }
        catch (Exception)
        {

            return null;
        }





    }

    #endregion

    #region Dispose 

    void IDisposable.Dispose()
    {
        if (_unitOfWork != null)
            _unitOfWork.DoctorReserveTimes.TryDisposeSafe();
    }




    #endregion

}
