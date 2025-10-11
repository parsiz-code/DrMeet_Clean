using DNTCommon.Web.Core;
using DrMeet.Api.Features.Licenses.DTOs;
using DrMeet.Api.Features.Sliders.DTOs;
using DrMeet.Api.Shared.Domian;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Licensess;

public interface ILicensesService : IDisposable
{
    #region Licenses

    Task<ReturnUiResult> CreateLicensesAsync(CreateLicensesDto dto);
    Task<ReturnUiResult> EditLicenses(UpdateLicensesDto dto);
    Task<ReturnUiResult> DeleteLicenses(int LicensesId);
    Task<GetLicensesDetailDto> GetLicenses(int LicensesId);
    Task<PagedList<GetLicensesDetailDto>> GetLicenses(GetLicensesByCenterIdResponseParams request);
    Task<PagedList<GetLicensesListDto>> GetLicenses(GetLicensesResponseParams request);

    #endregion
}
public class LicensesService : ILicensesService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public LicensesService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Licenses

    public async Task<ReturnUiResult> CreateLicensesAsync(CreateLicensesDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var LicensesCount = await _unitOfWork.Licenses.AsQueryable().CountAsync();

          Domain.Others.  Licenses Licenses = new Domain.Others.Licenses()
            {
                Name = dto.Name,
                Status = false,

                Order = LicensesCount + 1,

            };

            await _unitOfWork.Licenses.AddAsync(Licenses);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت مجوز با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت مجوز با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditLicenses(UpdateLicensesDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Licenses = await _unitOfWork.Licenses.GetByIdAsync(dto.Id);

            if (Licenses == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مجوز یافت نشد");
                return returnUiResult;
            }


            Licenses.Name = dto.Name;
            Licenses.Status = dto.Status;


            await _unitOfWork.Licenses.UpdateAsync(Licenses);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت مجوز با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت مجوز با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteLicenses(int LicensesId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Licenses = await _unitOfWork
                .Licenses
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == LicensesId);

            if (Licenses == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("مجوز یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.Licenses.DeleteAsync(LicensesId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف مجوز با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetLicensesDetailDto> GetLicenses(int LicensesId)
    {
        try
        {
            var Licenses = await _unitOfWork
                .Licenses
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == LicensesId)
                .Select(s => new GetLicensesDetailDto
                {
                    Name = s.Name,
                    Status = s.Status,
                    Order = s.Order,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (Licenses == null)
                return null;

            return Licenses;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<PagedList<GetLicensesDetailDto>> GetLicenses(GetLicensesByCenterIdResponseParams request)
    {
        try
        {
            var center = await _unitOfWork
               .Centers
               .GetByIdAsync(request.CenterId);

            if (center == null)
                return null;

            var licenses = await _unitOfWork
                 .Licenses
                 .GetAllAsync();

            var licenseDetail = center.CenterLicensesSelected
                
                .Select( s => new GetLicensesDetailDto
                {
                    Name =s.Licenses.Name,
                    Status = s.Licenses.Status,
                    Order = s.Licenses.Order,
                    Id = s.LicensesId.Value,
                })
                .ToList();

            if (licenseDetail == null)
                return null;
            PagedList<GetLicensesDetailDto> model = new();
            model.List = licenseDetail;
            model.Pagination = new PagedListInfo
            {
                PageNumber = request.PageNumber.Value,
                PageSize = request.PageSize.Value,
                TotalPages = (model.List.Count / request.PageSize.Value)+1,
                TotalCount = model.List.Count
            };
            return model;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<PagedList<GetLicensesListDto>> GetLicenses(GetLicensesResponseParams request)
    {
        try
        {
            var response = new PagedList<GetLicensesListDto>();

            var LicensesDtos = _unitOfWork
                 .Licenses
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                LicensesDtos = LicensesDtos.Where(_ => _.Name.Contains(request.Title));

            LicensesDtos = LicensesDtos.Where(_ => _.Status == (request.Status));


            var result = await LicensesDtos.ToPagedList(s => new GetLicensesListDto
            {
                Name = s.Name,
                Id = s.Id
            }, request.PageNumber, request.PageSize);
            //.AsNoTraking()




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
            _unitOfWork.Licenses.TryDisposeSafe();
    }


    #endregion

}
