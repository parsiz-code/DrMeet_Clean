using DNTCommon.Web.Core;
using DrMeet.Api.Features.Expertises.DTOs;
using DrMeet.Api.Features.Insurances.DTOs;
using DrMeet.Api.Shared.DTOs;
using DrMeet.Api.Shared.PagedList;
using DrMeet.Api.Shared.Persistence.UnitOfWork;
using MongoDB.Driver.Linq;

namespace DrMeet.Api.Shared.Services.Expertise;

public interface IExpertiseService : IDisposable
{
    #region Expertise
   
    Task<ReturnUiResult> CreateExpertiseAsync(CreateExpertiseDto dto);
    Task<ReturnUiResult> EditExpertise(UpdateExpertiseDto dto);
    Task<ReturnUiResult> DeleteExpertise(int ExpertiseId);
    Task<GetExpertiseDetailDto> GetExpertise(int ExpertiseId);

    Task<PagedList<GetExpertiseListDto>> GetExpertises(GetExpertiseResponseParams request);

    #endregion
}
public class ExpertiseService : IExpertiseService
{

    #region Constructor

    private readonly IUnitOfWork _unitOfWork;

    public ExpertiseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region Expertise

    public async Task<ReturnUiResult> CreateExpertiseAsync(CreateExpertiseDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var ExpertiseCount = await _unitOfWork.Expertise.AsQueryable().CountAsync();

          Domain.Others. Expertise Expertise = new Domain.Others.Expertise()
            {
                Name= dto.Name,

            };

            await _unitOfWork.Expertise.AddAsync(Expertise);

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

    public async Task<ReturnUiResult> EditExpertise(UpdateExpertiseDto dto)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Expertise = await _unitOfWork.Expertise.GetByIdAsync(dto.Id);

            if (Expertise == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نوع مرکز یافت نشد");
                return returnUiResult;
            }
         

            Expertise.Name = dto.Name;

         
            await _unitOfWork.Expertise.UpdateAsync(Expertise);

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
    public async Task<ReturnUiResult> DeleteExpertise(int ExpertiseId)
    {
        var returnUiResult = new ReturnUiResult();
        try
        {
            var Expertise = await _unitOfWork
                .Expertise
                .AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == ExpertiseId);

            if (Expertise == null)
            {
                returnUiResult.ReturnResult = ReturnResult.Error;
                returnUiResult.LstMessage.Add("نوع مرکز یافت نشد");
                return returnUiResult;
            }

            await _unitOfWork.Expertise.DeleteAsync(ExpertiseId);

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

    public async Task<GetExpertiseDetailDto> GetExpertise(int ExpertiseId)
    {
        try
        {
            var Expertise = await _unitOfWork
                .Expertise
                .AsQueryable()
                //.AsNoTraking()
                .Where(a => a.Id == ExpertiseId)
                .Select(s => new GetExpertiseDetailDto
                {
                    Name = s.Name,
                    Id = s.Id
                }).FirstOrDefaultAsync();

            if (Expertise == null)
                return null;

            return Expertise;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<PagedList<GetExpertiseListDto>> GetExpertises(GetExpertiseResponseParams request)
    {
        try
        {
            var response = new PagedList<GetExpertiseListDto>();

            var ExpertiseDtos = _unitOfWork
                 .Expertise
                 .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                ExpertiseDtos = ExpertiseDtos.Where(_ => _.Name.Contains(request.Title));


            var result = await ExpertiseDtos.ToPagedList(s => new GetExpertiseListDto
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
            _unitOfWork.Expertise.TryDisposeSafe();
    }


    #endregion

}
