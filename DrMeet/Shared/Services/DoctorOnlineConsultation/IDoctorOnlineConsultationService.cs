using DNTCommon.Web.Core;
using DrMeet.Api.Features.DoctorOnlineConsultations.DTOs;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Centers;
using Humanizer;
using MongoDB.Driver.Linq;
using System.Numerics;

namespace DrMeet.Api.Shared.Services.DoctorOnlineConsultation;

public interface IDoctorOnlineConsultationService : IDisposable
{
    #region DoctorOnlineConsultation

    Task<ReturnUiResult> CreateDoctorOnlineConsultationAsync(CreateDoctorOnlineConsultationRequestDto dto);
    Task<ReturnUiResult> EditDoctorOnlineConsultation(UpdateDoctorOnlineConsultationRequestDto dto);
    Task<ReturnUiResult> DeleteDoctorOnlineConsultation(int DoctorOnlineConsultationId);
    Task<GetDoctorOnlineConsultationDetailResponseDto> GetDoctorOnlineConsultation(int DoctorOnlineConsultationId);

    Task<PagedList<GetDoctorOnlineConsultationListResponseDto>> GetDoctorOnlineConsultations(GetDoctorOnlineConsultationRequestResponseParams request);
   
    #endregion
}
public class DoctorOnlineConsultationService : IDoctorOnlineConsultationService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public DoctorOnlineConsultationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region DoctorOnlineConsultation

    public async Task<ReturnUiResult> CreateDoctorOnlineConsultationAsync(CreateDoctorOnlineConsultationRequestDto dto)
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

            var service = await _unitOfWork.ProviderServices.GetByIdAsync(dto.ServicesAvailableId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }
            var DoctorOnlineConsultationCount = await _unitOfWork.CenterDoctorServiceOnlineConsultation.AsQueryable().CountAsync();

            CenterDoctorServiceOnlineConsultation DoctorOnlineConsultation = new CenterDoctorServiceOnlineConsultation()
            {
                DoctorId = dto.DoctorId,
                Doctor = doctor,
                ServicesAvailableId = dto.ServicesAvailableId,
   
                Price = dto.Price,
                Status = false,
                PercentagePayment= dto.PercentagePayment,
            };

            await _unitOfWork.CenterDoctorServiceOnlineConsultation.AddAsync(DoctorOnlineConsultation);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت تعرفه با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت تعرفه با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditDoctorOnlineConsultation(UpdateDoctorOnlineConsultationRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {



            var DoctorOnlineConsultation = await _unitOfWork.CenterDoctorServiceOnlineConsultation.GetByIdAsync(dto.Id);

            if (DoctorOnlineConsultation == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("تعرفه یافت نشد");
                return returnUiResult;
            }
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("دکتر یافت نشد");
                return returnUiResult;
            }

            var service = await _unitOfWork.ProviderServices.GetByIdAsync(dto.ServicesAvailableId);
            if (doctor == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }

            DoctorOnlineConsultation.DoctorId = dto.DoctorId;
            DoctorOnlineConsultation.Doctor = doctor;
            DoctorOnlineConsultation.ServicesAvailableId = dto.ServicesAvailableId;
         
            DoctorOnlineConsultation.Price = dto.Price;


            await _unitOfWork.CenterDoctorServiceOnlineConsultation.UpdateAsync(DoctorOnlineConsultation);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت تعرفه با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت تعرفه با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteDoctorOnlineConsultation(int DoctorOnlineConsultationId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var DoctorOnlineConsultation = await _unitOfWork
                .CenterDoctorServiceOnlineConsultation
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == DoctorOnlineConsultationId);

            if (DoctorOnlineConsultation == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("تعرفه یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.CenterDoctorServiceOnlineConsultation.DeleteAsync(DoctorOnlineConsultationId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف تعرفه با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetDoctorOnlineConsultationDetailResponseDto> GetDoctorOnlineConsultation(int DoctorOnlineConsultationId)
    {
        try
        {
            var DoctorOnlineConsultation = await _unitOfWork
                .CenterDoctorServiceOnlineConsultation
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == DoctorOnlineConsultationId)
                .Select(s => new GetDoctorOnlineConsultationDetailResponseDto
                {

                    Price = s.Price,
                    Status = s.Status,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (DoctorOnlineConsultation == null)
                return null;

            return DoctorOnlineConsultation;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetDoctorOnlineConsultationListResponseDto>> GetDoctorOnlineConsultations(GetDoctorOnlineConsultationRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetDoctorOnlineConsultationListResponseDto>();

            var DoctorOnlineConsultationDtos = _unitOfWork
                 .CenterDoctorServiceOnlineConsultation
                 .AsQueryable();
            if (request.DoctorId is not null)
                DoctorOnlineConsultationDtos = DoctorOnlineConsultationDtos.Where(_ => _.DoctorId.Equals(request.DoctorId));

            var result = await DoctorOnlineConsultationDtos.ToPagedList(s => new GetDoctorOnlineConsultationListResponseDto
            {
                Price = s.Price,
                Status = s.Status,
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

    #endregion

    #region Dispose 

    void IDisposable.Dispose()
    {
        if (_unitOfWork != null)
            _unitOfWork.CenterDoctorServiceOnlineConsultation.TryDisposeSafe();
    }


    #endregion

}
