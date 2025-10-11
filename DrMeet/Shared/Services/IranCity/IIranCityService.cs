using DNTCommon.Web.Core;
using DrMeet.Api.Features.Iran.EndPoints.DTOs;
using DrMeet.Api.Features.IranCitys.DTOs;
using DrMeet.Api.Features.IranProvinces.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.Extensions;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using DrMeet.Domain.Iran;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.IranCity;

public interface IIranCityService : IDisposable
{
    #region IranCity

    Task<ReturnUiResult> CreateIranCityAsync(CreateIranCityRequestDto dto);
    Task<ReturnUiResult> EditIranCity(UpdateIranCityRequestDto dto);
    Task<ReturnUiResult> DeleteIranCity(int IranCityId);
    Task<GetIranCityDetailResponseDto> GetIranCity(int IranCityId);

    Task<PagedList<GetIranCityListResponseDto>> GetIranCitys(GetIranCityRequestResponseParams request);

    #endregion
}
public class IranCityService : IIranCityService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public IranCityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region IranCity

    public async Task<ReturnUiResult> CreateIranCityAsync(CreateIranCityRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {

            if (!await _unitOfWork.IranProvinces.AsQueryable().Where(_ => _.Id == dto.ProvinceId).AnyAsync())
            {
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("استان وجود ندارد");
                return returnUiResult;
            }
            if (await _unitOfWork.IranCities.AsQueryable().Where(_ => _.Name == dto.Name&&_.ProvinceId==dto.ProvinceId).AnyAsync())
            {
                returnUiResult.ReturnResult = ReturnResult.Success;
                returnUiResult.LstMessage.Add("شهر وجود دارد");
                return returnUiResult;
            }
              Domain.Iran. IranCity IranCity = new Domain.Iran.IranCity()
            {
                Name = dto.Name,
                ProvinceId = dto.ProvinceId,

            };
            var province = await _unitOfWork.IranProvinces.GetByIdAsync(dto.ProvinceId);
            if (province != null)
            {
                province.Cities.Add(IranCity);
                await _unitOfWork.IranProvinces.UpdateAsync(province);
            }
            await _unitOfWork.IranCities.AddAsync(IranCity);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت شهر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت شهر با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditIranCity(UpdateIranCityRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var IranCity = await _unitOfWork.IranCities.GetByIdAsync(dto.Id);

            if (IranCity == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شهر یافت نشد");
                return returnUiResult;
            }


            IranCity.Name = dto.Name;


            await _unitOfWork.IranCities.UpdateAsync(IranCity);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت شهر با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت شهر با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteIranCity(int IranCityId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var IranCity = await _unitOfWork
                .IranCities
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == IranCityId);

            if (IranCity == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("شهر یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.IranCities.DeleteAsync(IranCityId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف شهر با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetIranCityDetailResponseDto> GetIranCity(int IranCityId)
    {
        try
        {
            var IranCity = await _unitOfWork
                .IranCities
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == IranCityId)
                .Select(s => new GetIranCityDetailResponseDto
                {
                    Name = s.Name,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (IranCity == null)
                return null;

            return IranCity;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetIranCityListResponseDto>> GetIranCitys(GetIranCityRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetIranCityListResponseDto>();

            var IranCityDtos = _unitOfWork
                 .IranCities
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                IranCityDtos = IranCityDtos.Where(_ => _.Name.Contains(request.Title));


            var result = await IranCityDtos.ToPagedList(s => new GetIranCityListResponseDto
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
            _unitOfWork.IranCities.TryDisposeSafe();
    }


    #endregion

}
