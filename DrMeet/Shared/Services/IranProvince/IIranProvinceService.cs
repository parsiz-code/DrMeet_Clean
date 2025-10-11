using DNTCommon.Web.Core;
using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.IranProvince;

public interface IIranProvinceService : IDisposable
{
    #region IranProvince

    Task<ReturnUiResult> CreateIranProvinceAsync(CreateIranProvinceRequestDto dto);
    Task<ReturnUiResult> EditIranProvince(UpdateIranProvinceRequestDto dto);
    Task<ReturnUiResult> DeleteIranProvince(int IranProvinceId);
    Task<GetIranProvinceDetailResponseDto> GetIranProvince(int IranProvinceId);

    Task<PagedList<GetIranProvinceListResponseDto>> GetIranProvinces(GetIranProvinceRequestResponseParams request);

    #endregion
}
public class IranProvinceService : IIranProvinceService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public IranProvinceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region IranProvince

    public async Task<ReturnUiResult> CreateIranProvinceAsync(CreateIranProvinceRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var IranProvinceCount = await _unitOfWork.IranProvinces.AsQueryable().CountAsync();
            if (await _unitOfWork.IranProvinces.AsQueryable().AnyAsync(_ => _.Name == dto.Name))
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("این استان قبلا ثبت شده");
                return returnUiResult;
            }

           Domain.Iran. IranProvince IranProvince = new Domain.Iran.IranProvince()
            {
                Name = dto.Name,

            };

            await _unitOfWork.IranProvinces.AddAsync(IranProvince);
            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت استان با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت استان با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditIranProvince(UpdateIranProvinceRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var IranProvince = await _unitOfWork.IranProvinces.GetByIdAsync(dto.Id);

            if (IranProvince == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("استان یافت نشد");
                return returnUiResult;
            }


            IranProvince.Name = dto.Name;


            await _unitOfWork.IranProvinces.UpdateAsync(IranProvince);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت استان با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت استان با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteIranProvince(int IranProvinceId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var IranProvince = await _unitOfWork
                .IranProvinces
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == IranProvinceId);

            if (IranProvince == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add(" استان یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.IranProvinces.DeleteAsync(IranProvinceId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف استان با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetIranProvinceDetailResponseDto> GetIranProvince(int IranProvinceId)
    {
        try
        {
            var IranProvince = await _unitOfWork
                .IranProvinces
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == IranProvinceId)
                .Select(s => new GetIranProvinceDetailResponseDto
                {
                    Name = s.Name,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (IranProvince == null)
                return null;

            return IranProvince;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetIranProvinceListResponseDto>> GetIranProvinces(GetIranProvinceRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetIranProvinceListResponseDto>();

            var IranProvinceDtos = _unitOfWork
                 .IranProvinces
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                IranProvinceDtos = IranProvinceDtos.Where(_ => _.Name.Contains(request.Title));


            var result = await IranProvinceDtos.ToPagedList(s => new GetIranProvinceListResponseDto
            {
                Name = s.Name,
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
            _unitOfWork.IranProvinces.TryDisposeSafe();
    }


    #endregion

}
