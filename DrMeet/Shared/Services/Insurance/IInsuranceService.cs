using DNTCommon.Web.Core;
using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Helpers;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Others;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Insurances;

public interface IInsuranceService : IDisposable
{
    #region Insurance

    Task<ReturnUiResult> CreateInsuranceAsync(CreateInsuranceRequestDto dto);
    Task<ReturnUiResult> EditInsurance(UpdateInsuranceRequestDto dto);
    Task<ReturnUiResult> DeleteInsurance(int InsuranceId);
    Task<GetInsuranceDetailResponseDto> GetInsurance(int InsuranceId);
    Task<ReturnUiResult> ToggleInsurance(int InsuranceId);
    Task<PagedList<GetInsuranceListResponseDto>> GetInsurances(GetInsuranceSelectListRequestResponseParams request);
    Task<PagedList<GetInsuranceListResponseDto>> GetInsurances(GetInsuranceRequestResponseParams request);

    #endregion
}
public class InsuranceService : IInsuranceService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public InsuranceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Insurance

    public async Task<ReturnUiResult> CreateInsuranceAsync(CreateInsuranceRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var InsuranceCount = await _unitOfWork.Insurances.AsQueryable().CountAsync();

            Insurance Insurance = new Insurance()
            {
                Name = dto.Name,
                IsBaseInsurance = dto.IsBaseInsurance,
                Picture = await FileUploadManager.UploadAsync(dto.Picture, FolderImagesType.Insurance),

                Status = dto.Status,
                Order = InsuranceCount + 1,

            };

            await _unitOfWork.Insurances.AddAsync(Insurance);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت بیمه با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت بیمه با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditInsurance(UpdateInsuranceRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Insurance = await _unitOfWork.Insurances.GetByIdAsync(dto.Id);

            if (Insurance == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("بیمه یافت نشد");
                return returnUiResult;
            }


            Insurance.Name = dto.Name;
            Insurance.Status = dto.Status;
            Insurance.IsBaseInsurance = dto.IsBaseInsurance;
            if (dto.Picture is not null)
            {

                Insurance.Picture = await FileUploadManager.UploadAsync(dto.Picture, FolderImagesType.Insurance);
            }

            await _unitOfWork.Insurances.UpdateAsync(Insurance);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت بیمه با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت بیمه با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteInsurance(int InsuranceId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Insurance = await _unitOfWork
                .Insurances
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == InsuranceId);

            if (Insurance == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("بیمه  یافت نشد");
                return returnUiResult;
            }

            await FileUploadManager.DeleteAsync(Insurance.Picture);

            await _unitOfWork.Insurances.DeleteAsync(InsuranceId);



            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف بیمه با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetInsuranceDetailResponseDto> GetInsurance(int InsuranceId)
    {
        try
        {
            var Insurance = await _unitOfWork
                .Insurances
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == InsuranceId)
                .Select(s => new GetInsuranceDetailResponseDto
                {
                    Name = s.Name,
                    Status = s.Status,
                    Order = s.Order,
                    Id = s.Id,
                    Picture = s.Picture,
                    IsBaseInsurance = s.IsBaseInsurance
                }).FirstOrDefaultAsync();

            if (Insurance == null)
                return null;

            return Insurance;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<ReturnUiResult> ToggleInsurance(int InsuranceId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Insurance = await _unitOfWork
                .Insurances
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == InsuranceId);

            if (Insurance == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("بیمه  یافت نشد");
                return returnUiResult;
            }

            Insurance.Status = !Insurance.Status;
            await _unitOfWork.Insurances.UpdateAsync(Insurance);



            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("وضیعیت بیمه با موفقیت تغییر  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<PagedList<GetInsuranceListResponseDto>> GetInsurances(GetInsuranceRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetInsuranceListResponseDto>();

            var InsuranceDtos = _unitOfWork
                 .Insurances
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                InsuranceDtos = InsuranceDtos.Where(_ => _.Name.Contains(request.Title) && _.IsBaseInsurance.Equals(request.IsBaseInsurance));
            InsuranceDtos = InsuranceDtos.Where(_ => _.IsBaseInsurance.Equals(request.IsBaseInsurance));


            var result = await InsuranceDtos.ToPagedList(s => new GetInsuranceListResponseDto
            {
                Name = s.Name,
                Id = s.Id,
                IsBaseInsurance = s.IsBaseInsurance,
                Picture = s.Picture,
                Status = s.Status

            }, request.PageNumber, request.PageSize);


            return result;
        }
        catch (Exception)
        {
            // ثبت لاگ
            return null;
        }
    }
    public async Task<PagedList<GetInsuranceListResponseDto>> GetInsurances(GetInsuranceSelectListRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetInsuranceListResponseDto>();

            var InsuranceDtos = _unitOfWork
                 .Insurances
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Q))
                InsuranceDtos = InsuranceDtos.Where(_ => _.Name.Contains(request.Q));
            InsuranceDtos = InsuranceDtos.Where(_ => _.IsBaseInsurance.Equals(request.IsBaseInsurance));


            var result = await InsuranceDtos.ToPagedList(s => new GetInsuranceListResponseDto
            {
                Name = s.Name,
                Id = s.Id,
                IsBaseInsurance = s.IsBaseInsurance,
                Picture = s.Picture,
                Status = s.Status

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
            _unitOfWork.Insurances.TryDisposeSafe();
    }


    #endregion

}
