using DNTCommon.Web.Core;
using DrMeet.Api.Features.Doctors.EndPoints.DTOs;
using DrMeet.Api.Features.DoctorsShifts.EndPoints.DTOs;

using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Doctors;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DrMeet.Api.Shared.Services.DoctorShifts.DoctorShiftService;
namespace DrMeet.Api.Shared.Services.DoctorShifts;
public interface IDoctorShiftService : IDisposable
{

    #region DoctorShift
    Task<int> GetDoctorId(int shiftId);
    Task<int> GetCenterId(int shiftId);
    // ثبت شیف
    Task<ReturnUiResult> CreateDoctorShiftAsync(CreateShiftRequestDto dto, int DoctorId);
    // گرفتن یک شیف
    Task<ShiftResposneDto> GetShift(int shiftId);
    // ویرایش شیف
    Task<ReturnUiResult> EditDoctorShift(UpdateShiftRequestDto dto, int DoctorId);

    Task<List<ShiftGroupResponseDto>> GetShifts(GetShiftDoctorRequestDto model);
    Task<GetDoctorCenterShiftResponseDto> GetShifts(GetShiftCenterRequestDto model);

    Task<ShiftResposneDto> GetShiftByDate(DayOfWeek date);

    Task<ReturnUiResult> DeleteShift(int shiftId, int DoctorId);

    #endregion

}
public class DoctorShiftService : IDoctorShiftService
{
 
    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public DoctorShiftService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region DoctorShift

    public async Task<ReturnUiResult> CreateDoctorShiftAsync(CreateShiftRequestDto dto, int DoctorId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }

            var centerDoctorsDepartmantSelected =  _unitOfWork.CenterDoctorsDepartmantSelected.AsQueryable().Where(_=>_.CenterDoctorsSelected.DoctorId == dto.DoctorId&&_.CenterDepartment.CenterId == dto.CenterId&&_.CenterDepartmentId==dto.DepartmantId).FirstOrDefaultAsync();
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }



            var doctorShiftDayExists = await _unitOfWork.DoctorShifts.AsQueryable().Where(_ => _.CenterDoctorsDepartmantId== centerDoctorsDepartmantSelected.Id && _.DayOfWeek == dto.DayOfWeek && _.ShiftType == dto.ShiftType  ).AnyAsync();
            if (doctorShiftDayExists)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شیفت در این روز موجود است");
                return returnUiResult;
            }
            var listTimes = GetVisitSlots(dto.StartTime, dto.EndTime, dto.MeetTime);
            DoctorShift ds = new DoctorShift()
            {
                DayOfWeek = dto.DayOfWeek,
                Description = dto.Description,
                CenterDoctorsDepartmantId = centerDoctorsDepartmantSelected.Id,

                EndTime = dto.EndTime,
                MeetTime = dto.MeetTime,
                StartTime = dto.StartTime,
                ActivityStatus = (ShiftActivityStatus)dto.ActivityStatus,
                ShiftType = (ShiftType)dto.ShiftType,
                DoctorShiftTimeItems = listTimes
            };
            //if (dto.CenterId.HasValue)
            //{
            //    var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId.Value);
            //    if (center != null)
            //        ds.CenterId = center.Id;
            //}


            await _unitOfWork.Doctors.UpdateAsync(doctor);
            await _unitOfWork.DoctorShifts.AddAsync(ds);
            // await _unitOfWork.SaveChange();

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("شیفت با موفقیت ثبت شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت شیفت با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public List<DoctorShiftTimeItem> GetVisitSlots(string StartTime, string EndTime, int MeetTime)
    {
        var slots = new List<DoctorShiftTimeItem>();

        var start = TimeOnly.Parse(StartTime);
        var end = TimeOnly.Parse(EndTime);
        var meetDuration = TimeSpan.FromMinutes(MeetTime);

        var current = start;

        while (current.Add(meetDuration) <= end)
        {
            var slotStart = current;
            var slotEnd = current.Add(meetDuration);
            string Id = Guid.NewGuid().ToString();
            slots.Add(new DoctorShiftTimeItem
            {
               
                EndTime = slotEnd.ToString("HH:mm"),
                StartTime = slotStart.ToString("HH:mm"),
                IsShiftAvailable = true,

            });
            current = slotEnd;
        }

        return slots;
    }

    public async Task<ReturnUiResult> EditDoctorShift(UpdateShiftRequestDto dto, int UserId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }

            var shift = await _unitOfWork.DoctorShifts.GetByIdAsync(dto.ShiftId);

            if (shift == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شیفت یافت نشد");
                return returnUiResult;

            }


            if (dto.CenterId.HasValue)
            {
                if (shift.CenterDoctorsDepartmant.CenterDepartment.CenterId.HasValue && !shift.CenterDoctorsDepartmant.CenterDepartment.CenterId.Value.Equals(dto.CenterId))
                {
                    returnUiResult.ReturnResult = ReturnResult.Error;
                    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                    return returnUiResult;
                }
            }
            if (!shift.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Equals(dto.DoctorId))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دسترسی یافت نشد");
                return returnUiResult;
            }
            if (dto.CenterId.HasValue)
            {
                var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId.Value);
                if (center != null)
                {
                    shift.CenterDoctorsDepartmant.CenterDepartment.CenterId = center.Id;
                }
            }

            shift.DayOfWeek = dto.DayOfWeek;
            shift.Description = dto.Description;
            shift.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId = dto.DoctorId;
            shift.EndTime = dto.EndTime;
            shift.MeetTime = dto.MeetTime;
            shift.StartTime = dto.StartTime;
            //   shift.Status = (ShiftStatus)dto.Status;
            shift.ActivityStatus = (ShiftActivityStatus)dto.ActivityStatus;
            shift.ShiftType = (ShiftType)dto.ShiftType;

            await _unitOfWork.DoctorShifts.UpdateAsync(shift);
            // await _unitOfWork.SaveChange();


            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("شیفت با موفقیت ویرایش شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ویرایش شیفت با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ShiftResposneDto> GetShift(int shiftId)
    {
        try
        {
            var shift = await _unitOfWork
                .DoctorShifts
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == shiftId)
                .Select(s => new ShiftResposneDto
                {
                    ShiftId = s.Id,
                    DayOfWeek = s.DayOfWeek,
                    Description = s.Description,
                    DoctorId = s.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Value,
                    EndTime = s.EndTime,
                    MeetTime = s.MeetTime,
                    StartTime = s.StartTime,
                    ActivityStatus = s.ActivityStatus,
                    ShiftType = s.ShiftType,
                }).FirstOrDefaultAsync();

            if (shift == null)
                return null;

            return shift;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<int> GetDoctorId(int shiftId)
    {
        try
        {
            var doctorId = await _unitOfWork
                .DoctorShifts
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == shiftId)
                .Select(s => s.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId).FirstOrDefaultAsync();

            if (doctorId == null)
                return 0;

            return doctorId??0;
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public async Task<int> GetCenterId(int shiftId)
    {
        try
        {
            var doctorId = await _unitOfWork
                .DoctorShifts
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == shiftId)
                .Select(s => s.CenterDoctorsDepartmant.CenterDepartment.CenterId).FirstOrDefaultAsync();

            if (doctorId == null)
                return 0;

            return doctorId??0;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<List<ShiftGroupResponseDto>> GetShifts(GetShiftDoctorRequestDto model)
    {



        try
        {

            var shiftDtos = _unitOfWork
                 .DoctorShifts
                 .AsQueryable();

            if (model.DayOfWeek.HasValue)
            {
                shiftDtos = shiftDtos.Where(_ => ((int)_.DayOfWeek) == ((int)model.DayOfWeek.Value));
            }
            var retunrShiftDtos = await shiftDtos.Where(_ => _.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Equals(model.DoctorId) && _.CenterDoctorsDepartmant.CenterDepartment.CenterId == null).GroupBy(a => a.DayOfWeek)
                   //.AsNoTraking()
                   .Select(s => new ShiftGroupResponseDto
                   {
                       DayOfWeek = s.Key,
                       Shifts = s.Select(_ => new ShiftResposneDto
                       {
                           ShiftId = _.Id,
                           DayOfWeek = _.DayOfWeek,
                           Description = _.Description,
                           DoctorId = _.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Value,
                           EndTime = _.EndTime,
                           MeetTime = _.MeetTime,
                           StartTime = _.StartTime,
                           ActivityStatus = _.ActivityStatus,
                           ShiftType = _.ShiftType,
                       }).ToList()
                   }
                   )
                   .ToListAsync();

            //var  shiftDtos1= await shiftDtos
            //.Select(s => new ShiftDto
            //{
            //    ShiftId = s.Id,
            //    DayOfWeek = s.DayOfWeek,
            //    Description = s.Description,
            //    DoctorId = s.DoctorId,
            //    EndTime = s.EndTime,
            //    MeetTime = s.MeetTime,
            //    StartTime = s.StartTime,
            //   // Status = s.Status,
            //    ActivityStatus = s.ActivityStatus,
            //    ShiftType = s.ShiftType
            //})
            //.ToListAsync();

            if (shiftDtos == null)
                return null;


            return retunrShiftDtos;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }

    }
    public async Task<GetDoctorCenterShiftResponseDto> GetShifts(GetShiftCenterRequestDto model)
    {



        try
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(model.DoctorId);

            var shiftDtos = _unitOfWork
                 .DoctorShifts
                 .AsQueryable().Where(_ => _.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Equals(model.DoctorId) && _.CenterDoctorsDepartmant.CenterDepartment.CenterId == model.CenterId);


            var s = shiftDtos.ToList();
            if (model.DayOfWeek.HasValue)
            {
                shiftDtos = shiftDtos.Where(_ => ((int)_.DayOfWeek) == ((int)model.DayOfWeek.Value));
            }
            var WorkDays = shiftDtos
                                .GroupBy(x => x.DayOfWeek)
                                .Select(dayGroup => new ShiftGroupResponseDto
                                {
                                    DayOfWeek = dayGroup.Key,
                                    Shifts = dayGroup.Select(s => new ShiftResposneDto
                                    {
                                        ShiftId = s.Id,
                                        DoctorId = s.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Value,
                                        Description = s.Description,
                                        StartTime = s.StartTime,
                                        EndTime = s.EndTime,
                                        MeetTime = s.MeetTime,
                                        ActivityStatus = s.ActivityStatus,
                                        ShiftType = s.ShiftType,
                                        DayOfWeek = s.DayOfWeek
                                    }).ToList()
                                }).ToList();

            var retunrShiftDtos = await shiftDtos
               //.AsNoTraking()
               .Select(s => new GetDoctorCenterShiftResponseDto
               {

                   WorkDays = WorkDays
               }
               )
               .FirstOrDefaultAsync()??new GetDoctorCenterShiftResponseDto();
            if (retunrShiftDtos is not null && retunrShiftDtos.doctorInfo is null)
                retunrShiftDtos.doctorInfo = new DoctorInfoDoctorCenterShiftDto();
            retunrShiftDtos.doctorInfo = new DoctorInfoDoctorCenterShiftDto { FullName = doctor.User.FullName };

            //  var result =  retunrShiftDtos.ToList();

            return retunrShiftDtos ?? new GetDoctorCenterShiftResponseDto();
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }

    }
    public async Task<ShiftResposneDto> GetShiftByDate(DayOfWeek date)
    {
        try
        {
            var shift = await _unitOfWork
               .DoctorShifts
               .AsQueryable()
               //.AsNoTraking()
               .Where(a => ((int)a.DayOfWeek) == ((int)date))
               .Select(s => new ShiftResposneDto
               {
                   ShiftId = s.Id,
                   DayOfWeek = s.DayOfWeek,
                   Description = s.Description,
                   DoctorId = s.CenterDoctorsDepartmant.CenterDoctorsSelected.DoctorId.Value,
                   EndTime = s.EndTime,
                   MeetTime = s.MeetTime,
                   StartTime = s.StartTime,
                   ActivityStatus = s.ActivityStatus,
                   ShiftType = s.ShiftType,
               }).FirstOrDefaultAsync();

            if (shift == null)
                return null;

            return shift;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ReturnUiResult> DeleteShift(int shiftId, int DoctorId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var shift = await _unitOfWork
                .DoctorShifts
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == shiftId);



            if (shift == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شیفت یافت نشد");
                return returnUiResult;

            }

            //if (!shift.CenterDoctorsDepartmant.DoctorId.Equals(DoctorId))
            //{
            //    returnUiResult.ReturnResult = ReturnResult.Error;
            //    returnUiResult.LstMessage.Add("دسترسی یافت نشد");
            //    return returnUiResult;
            //}
            await _unitOfWork.DoctorShifts.DeleteAsync(shiftId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("شیفت با موفقیت حذف شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("حذف شیفت با خطا مواجه شد");
            return returnUiResult;
        }
    }

    #endregion

    #region Dispose 

    void IDisposable.Dispose()
    {
        if (_unitOfWork != null)
            _unitOfWork.DoctorShifts.TryDisposeSafe();
    }

 

   
    #endregion

}