using DNTCommon.Web.Core;
using DrMeet.Api.Features.CenterTypes.DTOs;
using DrMeet.Api.Features.DoctorReserveTimes.DTOs;

using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.CenterType;

public interface ICenterTypeService : IDisposable
{
    #region CenterType
   
    Task<ReturnUiResult> CreateCenterTypeAsync(CreateCenterTypeRequestDto dto);
    Task<ReturnUiResult> EditCenterType(UpdateCenterTypeRequestDto dto);
    Task<ReturnUiResult> DeleteCenterType(int CenterTypeId);
    Task<GetCenterTypeDetailResponseDto> GetCenterType(int CenterTypeId);

    Task<PagedList<GetCenterTypeListResponseDto>> GetCenterTypes(GetCenterTypeRequestResponseParams request);

    #endregion
}
public class CenterTypeService : ICenterTypeService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public CenterTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region CenterType

    public async Task<ReturnUiResult> CreateCenterTypeAsync(CreateCenterTypeRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var CenterTypeCount = await _unitOfWork.CenterTypes.AsQueryable().CountAsync();

            DrMeet.Domain.Centers. CenterType centerType = new DrMeet.Domain.Centers.CenterType()
            {
                Name= dto.Name,
                Status=true,

                Order= CenterTypeCount+1,

            };

            await _unitOfWork.CenterTypes.AddAsync(centerType);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت نوع مرکز با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نوع مرکز با خطا مواجه شد");
            return returnUiResult;
        }
    }

    public async Task<ReturnUiResult> EditCenterType(UpdateCenterTypeRequestDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var CenterType = await _unitOfWork.CenterTypes.GetByIdAsync(dto.Id);

            if (CenterType == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نوع مرکز یافت نشد");
                return returnUiResult;
            }
         

            CenterType.Name = dto.Name;
            CenterType.Status = dto.Status;

         
            await _unitOfWork.CenterTypes.UpdateAsync(CenterType);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("ثبت نوع مرکز با موفقیت انجام شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("ثبت نوع مرکز با خطا مواجه شد");
            return returnUiResult;
        }
    }
    public async Task<ReturnUiResult> DeleteCenterType(int CenterTypeId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var CenterType = await _unitOfWork
                .CenterTypes
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == CenterTypeId);

            if (CenterType == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نوع مرکز یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.CenterTypes.DeleteAsync(CenterTypeId);

            returnUiResult.ReturnResult = ReturnResult.Success;
            returnUiResult.LstMessage.Add("حذف نوع مرکز با موفقیت انجام  شد");
            return returnUiResult;
        }
        catch (Exception)
        {
            returnUiResult.ReturnResult = ReturnResult.Error;
            returnUiResult.LstMessage.Add("خطا  رخ داد");
            return returnUiResult;
        }
    }

    public async Task<GetCenterTypeDetailResponseDto> GetCenterType(int CenterTypeId)
    {
        try
        {
            var CenterType = await _unitOfWork
                .CenterTypes
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == CenterTypeId)
                .Select(s => new GetCenterTypeDetailResponseDto
                {
                    Name = s.Name,
                    Status = s.Status,
                    Order = s.Order,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (CenterType == null)
                return null;

            return CenterType;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetCenterTypeListResponseDto>> GetCenterTypes(GetCenterTypeRequestResponseParams request)
    {
        try
        {
            var response = new PagedList<GetCenterTypeListResponseDto>();

            var CenterTypeDtos = _unitOfWork
                 .CenterTypes
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                CenterTypeDtos = CenterTypeDtos.Where(_ => _.Name.Contains(request.Title)&&_.Status.Equals(request.Status));

             CenterTypeDtos = CenterTypeDtos.Where(_ => _.Status.Equals(request.Status));


            var result = await CenterTypeDtos.ToPagedList(s => new GetCenterTypeListResponseDto
            {

                Name = s.Name,
                Id = s.Id,
                Status=s.Status,
                
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
            _unitOfWork.CenterTypes.TryDisposeSafe();
    }


    #endregion

}
