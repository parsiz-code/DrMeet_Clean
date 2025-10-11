using DNTCommon.Web.Core;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;

using DrMeet.Api.Features.DoctorTariffs.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Centers;
using Humanizer;
using MongoDB.Driver.Linq;
using System.Numerics;

namespace DrMeet.Api.Shared.Services.DoctorTariff;

public interface IDoctorTariffService : IDisposable
{
    #region DoctorTariff

    Task<ReturnUiResult> CreateDoctorTariffAsync(CreateDoctorTariffRequestDto dto);
    Task<ReturnUiResult> EditDoctorTariff(UpdateDoctorTariffRequestDto dto);
    Task<ReturnUiResult> DeleteDoctorTariff(int DoctorTariffId);
    Task<GetDoctorTariffDetailResponseDto> GetDoctorTariff(int DoctorTariffId);

    Task<PagedList<GetDoctorTariffDetailListResponseDto>> GetDoctorTariffs(GetDoctorTariffRequestResponseParams request);
   
    #endregion
}
public class DoctorTariffService : IDoctorTariffService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public DoctorTariffService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region DoctorTariff

    public async Task<ReturnUiResult> CreateDoctorTariffAsync(CreateDoctorTariffRequestDto dto)
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
            var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;
            }
            var service = await _unitOfWork.ProviderServices.GetByIdAsync(dto.ServicesAvailableId);
            if (service == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }
            var DoctorTariffCount = await _unitOfWork.CenterDoctorServicePricing.AsQueryable().CountAsync();

            CenterDoctorServicePricing DoctorTariff = new CenterDoctorServicePricing
            {
                DoctorId = dto.DoctorId,
                Doctor = doctor,
                CenterId = dto.DoctorId,
                Center = center,
                ProviderServicesId = dto.ServicesAvailableId,
               
                Price = dto.Price,
                Status = false,
               
                PercentagePayment= dto.PercentagePayment,
            };

            await _unitOfWork.CenterDoctorServicePricing.AddAsync(DoctorTariff);

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

    public async Task<ReturnUiResult> EditDoctorTariff(UpdateDoctorTariffRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {



            var DoctorTariff = await _unitOfWork.CenterDoctorServicePricing.GetByIdAsync(dto.Id);

            if (DoctorTariff == null)
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
            var center = await _unitOfWork.Centers.GetByIdAsync(dto.CenterId);
            if (center == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مرکز یافت نشد");
                return returnUiResult;
            }
            var service = await _unitOfWork.ProviderServices.GetByIdAsync(dto.ServicesAvailableId);
            if (service == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("خدمت یافت نشد");
                return returnUiResult;
            }

            DoctorTariff.DoctorId = dto.DoctorId;
            DoctorTariff.Doctor = doctor;
            DoctorTariff.CenterId = dto.CenterId;
            DoctorTariff.Center = center;
            DoctorTariff.ProviderServicesId = dto.ServicesAvailableId;
       
            DoctorTariff.Price = dto.Price;
            DoctorTariff.PercentagePayment = dto.PercentagePayment;


            await _unitOfWork.CenterDoctorServicePricing.UpdateAsync(DoctorTariff);

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
    public async Task<ReturnUiResult> DeleteDoctorTariff(int DoctorTariffId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var DoctorTariff = await _unitOfWork
                .CenterDoctorServicePricing
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == DoctorTariffId);

            if (DoctorTariff == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("تعرفه یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.CenterDoctorServicePricing.DeleteAsync(DoctorTariffId);

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

    public async Task<GetDoctorTariffDetailResponseDto> GetDoctorTariff(int DoctorTariffId)
    {
        try
        {
            var DoctorTariff = await _unitOfWork
                .CenterDoctorServicePricing
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == DoctorTariffId)
                .Select(s => new GetDoctorTariffDetailResponseDto
                {
                    PercentagePayment=s.PercentagePayment,
                    DoctorId = s.DoctorId.Value,
                    CenterId=s.CenterId.Value,
                    ServiceId=s.ProviderServicesId,
                    Price = s.Price,
               
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (DoctorTariff == null)
                return null;

            return DoctorTariff;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetDoctorTariffDetailListResponseDto>> GetDoctorTariffs(GetDoctorTariffRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetDoctorTariffDetailListResponseDto>();

            var DoctorTariffDtos = _unitOfWork
                 .CenterDoctorServicePricing
                 .AsQueryable();
            if (request.DoctorId is not null)
                DoctorTariffDtos = DoctorTariffDtos.Where(_ => _.DoctorId.Equals(request.DoctorId));

            var result = await DoctorTariffDtos.ToPagedList(s =>   new GetDoctorTariffDetailListResponseDto
            {

                DoctorName = s.Doctor.User.FirstName + " " + s.Doctor.User.LastName,
                ServiceName = s.ProviderServices.Name,
                Price = s.Price,
        
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
            _unitOfWork.CenterDoctorServicePricing.TryDisposeSafe();
    }


    #endregion

}
